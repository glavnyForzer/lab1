namespace Contracts
{
    public interface IRepositoryManager
    {
        ICompanyRepository Company { get; }
        IEmployeeRepository Employee { get; }
        IBoatRepository Boat { get; }
        IDriverRepository Driver { get; }
        void Save();
    }
}
