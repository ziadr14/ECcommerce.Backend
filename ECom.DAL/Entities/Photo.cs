using ECom.DAL.Entities.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.DAL.Entities
{
    public class Photo : Base
    {
        public int Id { get; set; }

        public string PhotoUrl { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }



    }
}
