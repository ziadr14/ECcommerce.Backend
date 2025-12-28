using AutoMapper;
using ECom.BLL.DTOs;
using ECom.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace ECom.BLL.Mapper
{
    public class Categorymapper:Profile
    {
        public Categorymapper()
        {

            CreateMap<CategoryDto, Category>().ReverseMap();

        }

    }
}
