using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace DeliveryAplication.Models
{
    public class Request
    {
        public int ID { get; set; }
        public int? ClientID { get; set; }
        public Client? Client { get; set; }
        public List<RequestProduct>? RequestProducts { get; set; }
        public double TotalPrice { get; set; }

        public int? DeliveryLocationID { get; set; }
        public Location? DeliveryLocation { get; set; }

        public double CalculateTotalPrice()
        {
            if (RequestProducts == null)
            {
                return 0;
            }

            double totalPrice = 0;

            foreach (var requestProduct in RequestProducts)
            {
                totalPrice += requestProduct.Product.Weight * requestProduct.Quantity;
            }

            return totalPrice;
        }
    }
}


