namespace Entities.RequestFeatures
{
    public class BoatParameters: RequestParameters
    {
        public BoatParameters() 
        {
            OrderBy = "brend";
        }

        public string FirstBoatBrand { get; set; } = "A";
        public string LastBoatBrand { get; set; } = "Z";
        public string SearchTerm { get; set; }

    }
}
