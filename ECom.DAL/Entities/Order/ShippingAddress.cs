namespace ECom.DAL.Entities.Order
{
    public class ShippingAddress
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
}