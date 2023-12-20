using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class DriverRepository: RepositoryBase<Capitan>, IDriverRepository
    {
        public DriverRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)        
        {
        }
    }
}
