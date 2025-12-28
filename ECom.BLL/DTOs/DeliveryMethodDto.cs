using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.BLL.DTOs
{
    public class DeliveryMethodDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string DeliveryTime { get; set; }
    }
    public class CreateDeliveryMethodDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string DeliveryTime { get; set; }
    }
    public class UpdateDeliveryMethodDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string DeliveryTime { get; set; }
    }

}
