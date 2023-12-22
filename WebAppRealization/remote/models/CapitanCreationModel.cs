using System.Collections.Generic;

namespace WebAppIImpl.remote.models
{
    public class CapitanCreationModel
    {
        public string? Name { get; set; }
        public string Address { get; set; }
        public IEnumerable<BoatModel> Boats { get; set; }
    }
}