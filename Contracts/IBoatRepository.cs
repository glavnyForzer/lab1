using Entities.Models;
using Entities.RequestFeatures;

namespace Contracts
{
    public interface IBoatRepository
    {
        Task<PagedList<Boat>> GetBoatsAsync(Guid capitanId, BoatParameters boatParameters, bool trackChanges);
        Task<Boat> GetBoatByIdAsync(Guid capitanId, Guid boatId, bool trackChanges);
        void CreateBoatForCapitan(Guid capitanId, Boat boat);
        void DeleteBoat(Boat boat);
    }
}
