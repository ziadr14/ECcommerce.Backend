using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.DAL.Entities
{
    public class AppUser : IdentityUser
    {
        public string DisoplayName { get; set; }

        public Address Address { get; set; }
    }
}
