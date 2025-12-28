using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.BLL.DTOs
{
    public class ProductParams
    {

        public int CategoryId { get; set; } = 0;
        public string? Sort { get; set; }
        public string? Search { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 3;
    }

}
