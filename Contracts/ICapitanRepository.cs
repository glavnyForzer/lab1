using Entities.Models;

namespace Contracts
{
    public interface ICapitanRepository
    {
        IEnumerable<Capitan> GetAllCapitans(bool trackChanges);
        public Capitan GetCapitan(Guid id, bool trackChanges);
    }
}
