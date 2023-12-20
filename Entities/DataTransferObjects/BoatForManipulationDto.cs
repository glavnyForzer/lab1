using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public abstract class BoatForManipulationDto
    {

        [Required(ErrorMessage = "Boat brend is a required field.")]
        [MaxLength(20, ErrorMessage = "Maximum length for the Brend is 20 characters.")]
        public string Brend { get; set; }

        [Required(ErrorMessage = "Boat model is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for rhe Model is 30 characte")]
        public string Model { get; set; }
    }
}
