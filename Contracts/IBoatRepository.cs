using Entities.Models;

namespace Contracts
{
    public interface IBoatRepository
    {
        Task<IEnumerable<Boat>> GetBoatsAsync(Guid capitanId, bool trackChanges);
        Task<Boat> GetBoatByIdAsync(Guid capitanId, Guid boatId, bool trackChanges);
        void CreateBoatForCapitan(Guid capitanId, Boat boat);
        void DeleteBoat(Boat boat);
    }
}
