using ECom.DAL.Entities.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.DAL.Entities
{
    public class Product : Base
    {
        public int Id { get; set; }

        public string Name { get; set; }    

        public string Description { get; set; }

        public decimal OldPrice { get; set; }

        public decimal NewPrice { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual List<Photo> Photos { get; set; }


    }
}
