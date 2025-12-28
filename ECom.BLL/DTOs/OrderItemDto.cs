using ECom.DAL.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.BLL.DTOs
{
    public class OrderItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public string Description { get; set; }
        public string Img { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public class OrderResponseDto
    {
        public int Id { get; set; }
        public string? BuyerEmail { get; set; }

        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public DateTime OrderDate { get; set; }

        public string DeliveryMethod { get; set; }
        public decimal DeliveryPrice { get; set; }

        public ShippingAddressDto ShippingAddress { get; set; }
        public IReadOnlyList<OrderItemDto> OrderItems { get; set; }

        public Status Status { get; set; } = Status.Pending;
    }

    public class ShippingAddressDto
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string? BuyerEmail { get; set; }

        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
    }
    public class CreateOrderDto
    {
        public string BasketId { get; set; }
        public int DeliveryMethodId { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public ShippingAddress ShippingAddress { get; set; }
    }



}
