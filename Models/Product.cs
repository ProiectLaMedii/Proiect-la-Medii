using System.ComponentModel.DataAnnotations;

namespace DeliveryAplication.Models
{
    public class Product
    {
        public int ID { get; set; }


        [StringLength(100, MinimumLength = 2)]
        [Display(Prompt = "Enter product name")]
        public string Name { get; set; }

        [StringLength(200)]
        [Display(Prompt = "Enter product description")]
        public string Description { get; set; }

        [Range(0, 10000)]
        [Display(Prompt = "Enter product weight")]
        public double Weight { get; set; }

        [StringLength(3)]
        [Display(Prompt = "Choose a size category: S/M/L/XL/XXL")]
        public string Size { get; set; }

        public ICollection<RequestProduct>? RequestProducts { get; set; }

        public int LocationID { get; set; }
        public Location? Location { get; set; }
    }
}