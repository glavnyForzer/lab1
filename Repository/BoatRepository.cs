using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class BoatRepository: RepositoryBase<Boat>, IBoatRepository
    {
        public BoatRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public async Task<PagedList<Boat>> GetBoatsAsync(Guid capitanId, BoatParameters boatParameters, bool trackChanges)
        {
            var boats = await FindByCondition(c => c.CapitanId.Equals(capitanId) && (c.Brend[0] >= boatParameters.FirstBoatBrand[0] && c.Brend[0] <= boatParameters.LastBoatBrand[0]),
                trackChanges).OrderBy(e => e.Brend).ToListAsync();
            return PagedList<Boat>.ToPagedList(boats, boatParameters.PageNumber, boatParameters.PageSize);
        }
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
