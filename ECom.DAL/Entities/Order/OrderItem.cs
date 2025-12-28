namespace ECom.DAL.Entities.Order
{
    public class OrderItem
    {
        public int Id { get; set; }


        public int ProductId { get; set; }

        public string ProductName { get; set; }
        public string Img { get; set; } = null!;

        public decimal Price { get; set; }

        public int Quantity { get; set; }


    }
}