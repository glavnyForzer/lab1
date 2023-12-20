using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class CapitanRepository: RepositoryBase<Capitan>, ICapitanRepository
    {
        public CapitanRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)        
        {
        }

        public IEnumerable<Capitan> GetAllCapitans(bool trackChanges) => FindAll(trackChanges).OrderBy(c => c.Name).ToList();
        public Capitan GetCapitan(Guid id, bool trackChanges) => FindByCondition(c => c.Id.Equals(id), trackChanges).SingleOrDefault(); 
    }
}
