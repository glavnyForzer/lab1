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

        public IEnumerable<Boat> GetBoats(Guid capitanId, bool trackChanges) => 
            FindByCondition(c => c.CapitanId.Equals(capitanId), trackChanges).OrderBy(e => e.Brend);
        public Boat GetBoatById(Guid capitanId, Guid id, bool trackChanges) => FindByCondition(c => c.CapitanId.Equals(capitanId) &&
            c.Id.Equals(id), trackChanges).SingleOrDefault();
    }
}
