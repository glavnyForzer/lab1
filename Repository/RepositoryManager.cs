using Contracts;
using Entities;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private ICompanyRepository _companyRepository;
        private IEmployeeRepository _employeeRepository;
        private IBoatRepository _boatRepository;
        private ICapitanRepository _capitanRepository;
        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public ICompanyRepository Company
        {
            get
            {
                if (_companyRepository == null)
                    _companyRepository = new CompanyRepository(_repositoryContext);
                return _companyRepository;
            }
        }
        public IEmployeeRepository Employee
        {
            get
            {
                if (_employeeRepository == null)
                    _employeeRepository = new EmployeeRepository(_repositoryContext);
                return _employeeRepository;
            }
        }
        public IBoatRepository Boat
        {
            get
            {
                if (_boatRepository == null)
                    _boatRepository = new BoatRepository(_repositoryContext);
                return _boatRepository;
            }
        }
        public ICapitanRepository Capitan
        {
            get
            {
                if (_capitanRepository == null)
                    _capitanRepository = new CapitanRepository(_repositoryContext);
                return _capitanRepository;
            }
        }
        public Task SaveAsync() => _repositoryContext.SaveChangesAsync();
    }
}