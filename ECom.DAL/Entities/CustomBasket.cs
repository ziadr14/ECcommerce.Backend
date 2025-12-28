using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.DAL.Entities
{
    public class CustomBasket
    {

        public CustomBasket() { }
        public CustomBasket(string id)
        {
            Id = id;
        }

        public string Id { get; set; } = null!;
        public List<BasketItem> Items { get; set; } = new();
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }

        public int? DeliveryMethodId { get; set; }
        public decimal ShippingPrice { get; set; }
        public decimal Total =>
            Items.Sum(i => i.Price * i.Quantity);
    }
}
