﻿using Entities.Models;

namespace Contracts
{
    public interface ICapitanRepository
    {
        IEnumerable<Driver> GetAllDrivers(bool trackChanges);
        public Driver GetDriver(Guid id, bool trackChanges);
        void CreateDriver(Driver driver);       
        IEnumerable<Driver> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteDriver(Driver driver);
    }
}
