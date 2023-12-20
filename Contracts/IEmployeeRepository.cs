﻿using Entities.Models;

namespace Contracts
{
    public interface IEmployeeRepository
    {
       public void Create(Employee employee);
        IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanges);
        Employee GetEmployee(Guid companyId, Guid id, bool trackChanges);
        void CreateEmployeeForCompany(Guid companyId, Employee employee);

    }
}
