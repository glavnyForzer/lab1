namespace Contracts
{
    public interface IRepositoryManager
    {
        ICompanyRepository Company { get; }
        IEmployeeRepository Employee { get; }
        IBoatRepository Boat { get; }
        ICapitanRepository Capitan { get; }
        void Save();
    }
}
