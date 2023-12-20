namespace Entities.DataTransferObjects
{
    public class CapitanForUpdateDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public IEnumerable<BoatForCreationDto> Boats { get; set; }
    }
}
