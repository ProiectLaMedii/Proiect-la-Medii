using System.ComponentModel.DataAnnotations;

namespace DeliveryAplication.Models
{
    public class Driver
    {
        public int ID { get; set; }


        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; }


        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [StringLength(100)]
        public string VehicleDetails { get; set; }
    }
}
