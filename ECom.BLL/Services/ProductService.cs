using AutoMapper;
using ECom.BLL.DTOs;
using ECom.BLL.DTOs.Pagination;
using ECom.BLL.Interfaces;
using ECom.DAL.Entities;
using ECom.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _image;

        public ProductService(IMapper mapper , IUnitOfWork unitOfWork , IImageService image)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _image = image;
        }




        public async Task<PaginationResopnse<ProductDto>> GetAll(ProductParams productParams)
        {
            var productsQuery = _unitOfWork.Products
                .GetAllQueryable()
                .Include(p => p.Photos)
                .Include(p => p.Category)
                .AsNoTracking();

            if (!string.IsNullOrEmpty(productParams.Search))
            {
                var searchWords = productParams.Search.Split(' ');

                productsQuery = productsQuery.Where(m => searchWords.All( word =>
               
                    m.Name.ToLower().Contains(word.ToLower()) ||
                    m.Description.ToLower().Contains(word.ToLower())

                    ));

            }



            if (productParams.CategoryId > 0)
            {
                productsQuery = productsQuery
                    .Where(p => p.CategoryId == productParams.CategoryId);
            }



            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                productsQuery = productParams.Sort switch
                {
                    "PriceAce" => productsQuery.OrderBy(p => p.NewPrice),
                    "PriceDce" => productsQuery.OrderByDescending(p => p.NewPrice),
                    _ => productsQuery.OrderBy(p => p.Name)
                };

            }
            else
            {
                productsQuery = productsQuery.OrderBy(p => p.Name);
            }

            var totalCount = await productsQuery.CountAsync();

            productsQuery = productsQuery
                .Skip((productParams.PageNumber - 1) * productParams.PageSize)
                .Take(productParams.PageSize);


            var products = await productsQuery.ToListAsync();

            
            var mapped = _mapper.Map<IReadOnlyList<ProductDto>>(products);
            return new PaginationResopnse<ProductDto>(
                productParams.PageNumber,
                productParams.PageSize,
                totalCount,
                mapped
            );

        }



        public async Task<ProductDto> GetById(int id)
        {
            var productQuery = await _unitOfWork.Products.GetByIdQueryable(id); 

            var product = await productQuery.Include(p => p.Photos).Include(p => p.Category).FirstOrDefaultAsync();

            return _mapper.Map<ProductDto >(product);


        }


        public async Task<PaginationResopnse<ProductDto>> GetSimilarProducts(
            int productId,
            ProductParams productParams
        )
        {
            var product = await _unitOfWork.Products
                .GetAllQueryable()
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
            {
                return new PaginationResopnse<ProductDto>(
                    productParams.PageNumber,
                    productParams.PageSize,
                    0,
                    new List<ProductDto>()
                );
            }

            var query = _unitOfWork.Products
                .GetAllQueryable()
                .AsNoTracking()
                .Include(p => p.Photos)
                .Include(p => p.Category)
                .Where(p =>
                    p.CategoryId == product.CategoryId &&
                    p.Id != productId
                );

            var totalCount = await query.CountAsync();

            var products = await query
                .Skip((productParams.PageNumber - 1) * productParams.PageSize)
                .Take(productParams.PageSize)
                .ToListAsync();

            var mapped = _mapper.Map<IReadOnlyList<ProductDto>>(products);

            return new PaginationResopnse<ProductDto>(
                productParams.PageNumber,
                productParams.PageSize,
                totalCount,
                mapped
            );
        }





        public async Task CreateProduct(CreateProductDto dto)
        {
            string folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "productPhotos");
            var photoNames = await _image.AddImageAsync(dto.Photos, folder);

            var product = _mapper.Map<Product>(dto);

            product.Photos = photoNames
                .Select(name => new Photo
                {
                    PhotoUrl = name
                })
                .ToList();

            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.CompleteAsync();
        }


        public async Task UpdateProduct(UpdateProductDto updateproductDto)
        {
            var productQuery = await _unitOfWork.Products.GetByIdQueryable(updateproductDto.Id);

            var product = await productQuery
                .Include(p => p.Photos)
                .FirstOrDefaultAsync();

            if (product == null)
                throw new Exception("Product not found");

            product.Photos ??= new List<Photo>();


            _mapper.Map(updateproductDto, product);


            string folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "productPhotos");

            foreach (var oldPhoto in product.Photos.ToList())
            {
                _image.DeleteImage(oldPhoto.PhotoUrl, folder);  

                await _unitOfWork.Photos.DeleteAsync(oldPhoto.Id); 
            }

            product.Photos.Clear();


            if (updateproductDto.Photos != null && updateproductDto.Photos.Count > 0)
            {
                var newPhotos = await _image.AddImageAsync(updateproductDto.Photos, folder);

                foreach (var path in newPhotos)
                {
                    product.Photos.Add(new Photo
                    {
                        PhotoUrl = path,
                        ProductId = product.Id
                    });
                }
            }


            await _unitOfWork.Products.UpdateAsync(product);
            await _unitOfWork.CompleteAsync();
        }



        public async Task DeleteProduct(int id)
        {
            var productQuery = await _unitOfWork.Products.GetByIdQueryable(id);

            var product = await productQuery
                .Include(p => p.Photos)
                .FirstOrDefaultAsync();

            if (product == null)
                throw new Exception("Product Not Found");

            string folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "productPhotos");

            foreach (var photo in product.Photos.ToList())
            {
                _image.DeleteImage(photo.PhotoUrl, folder);

                await _unitOfWork.Photos.DeleteAsync(photo.Id);
            }

            await _unitOfWork.Products.DeleteAsync(id);

            await _unitOfWork.CompleteAsync();
        }




    }
}
