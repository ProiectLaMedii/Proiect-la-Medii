using System.ComponentModel.DataAnnotations;

namespace DeliveryAplication.Models
{
    public class Pet
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        // am vrut sa scriu rasa dar las asa 
        [Display(Name = "Race")]
        public string Type { get; set; } 

        [StringLength(200)]
        [Display(Name = "Allergies or Special Considerations")]
        public string? Description { get; set; }

       
        public int? ClientID { get; set; }
        public Client? Client { get; set; }
    }
}
