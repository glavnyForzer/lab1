using Entities.Models;

namespace Contracts
{
    public interface ICapitanRepository
    {
        IEnumerable<Capitan> GetAllCapitans(bool trackChanges);
        public Capitan GetCapitan(Guid id, bool trackChanges);
        void CreateCapitan(Capitan capitan);       
        IEnumerable<Capitan> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
    }
}
