﻿using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class CapitanRepository: RepositoryBase<Driver>, ICapitanRepository
    {
        public CapitanRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)        
        {
        }

        public IEnumerable<Driver> GetAllDrivers(bool trackChanges) => FindAll(trackChanges).OrderBy(c => c.Name).ToList();
        public Driver GetDriver(Guid id, bool trackChanges) => FindByCondition(c => c.Id.Equals(id), trackChanges).SingleOrDefault();
        public void CreateDriver(Driver driver) => Create(driver);
        public IEnumerable<Driver> GetByIds(IEnumerable<Guid> ids, bool trackChanges) => FindByCondition(x => ids.Contains(x.Id), trackChanges).ToList();
        public void DeleteDriver(Driver driver) => Delete(driver);
    }
}
