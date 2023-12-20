using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class BoatRepository: RepositoryBase<Boat>, IBoatRepository
    {
        public BoatRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public IEnumerable<Boat> GetBoats(Guid driverId, bool trackChanges) => 
            FindByCondition(c => c.DriverId.Equals(driverId), trackChanges).OrderBy(e => e.Brend);
        public Boat GetBoatById(Guid driverId, Guid id, bool trackChanges) => FindByCondition(c => c.DriverId.Equals(driverId) &&
            c.Id.Equals(id), trackChanges).SingleOrDefault();
        public void CreateBoatForDriver(Guid driverId, Boat boat)
        {
            boat.DriverId = driverId;
            Create(boat);
        }
        public void DeleteBoat(Boat boat) => Delete(boat);
    }
}
