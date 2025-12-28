using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.DAL.Entities.BaseEntity
{
    public class Base
    {
        public bool isActive { get; set; } = true;
        public bool isDeleted { get; set; } = false;
    }
}
