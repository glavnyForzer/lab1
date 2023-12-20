using Entities.Models;

namespace Contracts
{
    public interface ICapitanRepository
    {
        Task<IEnumerable<Capitan>> GetAllCapitansAsync(bool trackChanges);
        public Task<Capitan> GetCapitanAsync(Guid id, bool trackChanges);
        void CreateCapitan(Capitan capitan);       
        Task<IEnumerable<Capitan>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteCapitan(Capitan capitan);
    }
}
