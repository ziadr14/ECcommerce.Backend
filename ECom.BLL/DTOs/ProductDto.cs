using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.BLL.DTOs
{
    public record ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }

        public List<PhotoDto> Photos { get; set; } 
        public string CategoryName { get; set; }
        public string ProductTypeName { get; set; }
        public bool isActive { get; set; } = true;
        public bool isDeleted { get; set; } = false;
    }

    public record CreateProductDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }

        public IFormFileCollection Photos { get; set; }
        public int CategoryId { get; set; }
        public int ProductTypeId { get; set; }
        public bool isActive { get; set; } = true;
        public bool isDeleted { get; set; } = false;
    }


    public record UpdateProductDto:CreateProductDto
    {
        public int Id { get; set; }
    }

    public record PhotoDto
    {
        public string PhotoUrl { get; set; }
        public int ProductId { get; set; }

        public bool isActive { get; set; } = true;
        public bool isDeleted { get; set; } = false;


    }
}
