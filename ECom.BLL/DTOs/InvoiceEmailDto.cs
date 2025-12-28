namespace ECom.BLL.DTOs
{
    public class InvoiceEmailDto
    {
        public string BuyerEmail { get; set; }
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string DeliveryMethod { get; set; }
        public decimal DeliveryPrice { get; set; }
        public decimal Total { get; set; }
        public List<InvoiceItemDto> Items { get; set; }
    }

    public class InvoiceItemDto
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
