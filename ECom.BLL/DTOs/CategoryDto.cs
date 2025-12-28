using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.BLL.DTOs
{
    public record CategoryDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool isActive { get; set; } = true;
        public bool isDeleted { get; set; } = false;
    }
}
