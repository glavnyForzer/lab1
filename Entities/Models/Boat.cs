using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Boat
    {
        [Column("BoatId")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Boat brend is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string Brend { get; set; }
        [Required(ErrorMessage = "Boat model is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for rhe Address is 30 characte")]
        public string Model { get; set; }

        [ForeignKey(nameof(Driver))]
        public Guid DriverId { get; set; }
        public Capitan Driver { get; set; }

    }
}
