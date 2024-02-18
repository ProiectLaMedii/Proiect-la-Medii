namespace DeliveryAplication.Models
{
    public class Delivery
    {
        public int ID { get; set; }
        public string OrderNumber { get; set; }
        public DateTime DispatchTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string DeliveryStatus { get; set; }
        public int? ClientID { get; set; }
        public Client? Client { get; set; }
        public int DriverID { get; set; }
        public Driver? Driver { get; set; }

        public int PickupLocationID { get; set; }
        public Location? PickupLocation { get; set; }
        public int DeliveryLocationID { get; set; }
        public Location? DeliveryLocation { get; set; }

    }
}
