using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class CapitanRepository: RepositoryBase<Capitan>, IcapitanRepository
    {
        public CapitanRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)        
        {
        }
    }
}
