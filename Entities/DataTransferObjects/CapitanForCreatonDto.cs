using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class CapitanForCreatonDto
    {
        [Required(ErrorMessage = "Capitan name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Capitan address is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for rhe Address is 60 characte")]
        public string Address { get; set; }
        public IEnumerable<BoatForCreationDto> Boats { get; set; }
    }
}
