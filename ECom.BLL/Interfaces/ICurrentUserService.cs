using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.BLL.Interfaces
{
    public interface ICurrentUserService
    {
        string BuyerEmail { get; }
    }
}
