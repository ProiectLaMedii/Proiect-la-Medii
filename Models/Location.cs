namespace DeliveryAplication.Models
{
    public class Location
    {
        public int ID { get; set; }
        public string LocationName { get; set; }

        public string Picture { get; set; }
        public string Address { get; set; }

        public ICollection<Delivery>? Delivery { get; set; }

        public ICollection<Delivery>? PickupDeliveries { get; set; }
        public ICollection<Delivery>? DropoffDeliveries { get; set; }

    }
}
