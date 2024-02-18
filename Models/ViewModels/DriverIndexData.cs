using System.Security.Policy;
using DeliveryAplication.Models;


namespace DeliveryAplication.Models.ViewModels
{
    public class LocationIndexData
    {
        public IEnumerable<Location> Locations { get; set; }
        public IEnumerable<Delivery> Delivery { get; set; } // Only Delivery property
    }
}
