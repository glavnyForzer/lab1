namespace Entities.DataTransferObjects
{
    public class DriverForUpdateDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public IEnumerable<BoatForCreationDto> Boats { get; set; }
    }
}
