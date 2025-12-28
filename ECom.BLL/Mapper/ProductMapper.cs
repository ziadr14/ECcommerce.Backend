using AutoMapper;
using ECom.BLL.DTOs;
using ECom.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.BLL.Mapper
{
    public class ProductMapper:Profile
    {
        public ProductMapper()
        {

            CreateMap<Product, ProductDto>()
                .ForMember(x => x.CategoryName, op => op.MapFrom(src => src.Category.Name))
                .ForMember(x => x.Photos, op => op.MapFrom(src => src.Photos)).ReverseMap();



            CreateMap<CreateProductDto, Product>()
                .ForMember(dest => dest.Photos, opt => opt.Ignore());

            CreateMap<UpdateProductDto, Product>()
    .ForMember(dest => dest.Photos, opt => opt.Ignore());

            CreateMap<PhotoDto , Photo>().ReverseMap();

        }
    }
}
