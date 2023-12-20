using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class BoatRepository: RepositoryBase<Boat>, IBoatRepository
    {
        public BoatRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Boat>> GetBoatsAsync(Guid capitanId, bool trackChanges) => 
            await FindByCondition(c => c.CapitanId.Equals(capitanId), trackChanges).OrderBy(e => e.Brend).ToListAsync();
        public async Task<Boat> GetBoatByIdAsync(Guid capitanId, Guid id, bool trackChanges) => await FindByCondition(c => c.CapitanId.Equals(capitanId) &&
            c.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
        public void CreateBoatForCapitan(Guid capitanId, Boat boat)
        {
            boat.CapitanId = capitanId;
            Create(boat);
        }
        public void DeleteBoat(Boat boat) => Delete(boat);
    }
}
