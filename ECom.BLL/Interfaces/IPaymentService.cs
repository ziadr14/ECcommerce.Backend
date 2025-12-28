using ECom.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.BLL.Interfaces
{
    public interface IPaymentService
    {
        Task<CustomBasket?> CreateOrUpdatePayment(
            string basketId,
            int? deliveryMethodId);

        Task<string> CreateStripeCheckoutSession(string basketId, int deliveryMethodId);

    }
}
