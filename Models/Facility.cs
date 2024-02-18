using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System.Security.Policy;

namespace DeliveryAplication.Models
{
    public class Facility
    {
        public int ID { get; set; }
        [Display(Name = "Room Number")]
        public string Room { get; set; }
        
        public decimal Price { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Available From:")]
        public DateTime AvailableDate { get; set; }
        public string Picture { get; set; }
        public int? LocationID { get; set; }
        public  Location? Location { get; set; }

        


    }
}
