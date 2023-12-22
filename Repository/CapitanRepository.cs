using System;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class CapitanRepository: RepositoryBase<Capitan>, ICapitanRepository
    {
        public CapitanRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)        
        {
        }

        public async Task<IEnumerable<Capitan>> GetAllCapitansAsync(bool trackChanges) => await FindAll(trackChanges).OrderBy(c => c.Name).ToListAsync();
        public async Task<Capitan> GetCapitanAsync(Guid id, bool trackChanges) => await FindByCondition(c => c.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
        public void CreateCapitan(Capitan capitan) => Create(capitan);
        public async Task<IEnumerable<Capitan>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) => await FindByCondition(x => ids.Contains(x.Id), trackChanges).ToListAsync();
        public void DeleteCapitan(Capitan capitan) => Delete(capitan);
    }
}
