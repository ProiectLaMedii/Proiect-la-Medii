namespace DeliveryAplication.Models
{
    public class RequestProduct
    {
        public int RequestID { get; set; }
        public Request? Request { get; set; }
        public int ProductID { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }
    }
}