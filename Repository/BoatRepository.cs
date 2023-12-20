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
    }
}
