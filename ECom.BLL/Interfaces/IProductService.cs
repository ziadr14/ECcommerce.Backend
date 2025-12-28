using ECom.BLL.DTOs;
using ECom.BLL.DTOs.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.BLL.Interfaces
{
    public interface IProductService
    {
        Task<PaginationResopnse<ProductDto>> GetAll(ProductParams productParams);

         Task<PaginationResopnse<ProductDto>> GetSimilarProducts(
            int productId,
            ProductParams productParams
        );

        Task<ProductDto> GetById(int id);

        Task CreateProduct(CreateProductDto createProductDto);

        Task UpdateProduct(UpdateProductDto updateproductDto);

        Task DeleteProduct(int id);
    }
}
