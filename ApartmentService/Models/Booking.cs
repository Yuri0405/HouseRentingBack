using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApartmentService.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("Apartment")]
        public int ApartmentId { get; set; }

        [Required]
        public string UserId { get; set; } 

        [Required]
        public DateTime StartDate { get; set; } 

        public DateTime EndDate { get; set; } 

        public bool IsConfirmed { get; set; } = false; 

        public Apartment Apartment { get; set; }
    }
}
