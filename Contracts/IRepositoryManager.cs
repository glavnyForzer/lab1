namespace Contracts
{
    public interface IRepositoryManager
    {
        ICompanyRepository Company { get; }
        IEmployeeRepository Employee { get; }
        IBoatRepository Boat { get; }
        IcapitanRepository Capitan { get; }
        void Save();
    }
}
