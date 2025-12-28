using ECom.DAL.Entities.Order;

public class Orders
{
    public int Id { get; set; }

    public string BuyerEmail { get; set; }

    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    public Status Status { get; set; }

    public PaymentMethod PaymentMethod { get; set; }   

    public string? PaymentIntentId { get; set; }       
    public string? StripeSessionId { get; set; }       

    public decimal SubTotal { get; set; }

    public decimal ShippingPrice { get; set; }

    public decimal Total => SubTotal + ShippingPrice;

    public ShippingAddress ShippingAddress { get; set; }

    public DeliveryMethod DeliveryMethod { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; }
}
