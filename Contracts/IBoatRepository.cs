using Entities.Models;

namespace Contracts
{
    public interface IBoatRepository
    {
        IEnumerable<Boat> GetBoats(Guid driverId, bool trackChanges);
        Boat GetBoatById(Guid driverId, Guid boatId, bool trackChanges);
        void CreateBoatForDriver(Guid driverId, Boat boat);
        void DeleteBoat(Boat boat);
    }
}
