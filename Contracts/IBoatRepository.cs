using Entities.Models;

namespace Contracts
{
    public interface IBoatRepository
    {
        IEnumerable<Boat> GetBoats(Guid capitanId, bool trackChanges);
        Boat GetBoatById(Guid capitanId, Guid boatId, bool trackChanges);
    }
}
