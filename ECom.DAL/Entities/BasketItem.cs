namespace ECom.DAL.Entities
{
    public class BasketItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string Description { get; set; }

        public string Img { get; set; } = null!;
        

        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}