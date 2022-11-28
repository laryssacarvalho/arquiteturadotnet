using ToDo.Domain.Entities;

namespace ToDo.Domain.Interface
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetAllAsync();
        Task<Item?> GetByIdAsync(Guid id);
        Task AddAsync(Item item);
        Task EditAsync(Item item);
        Task DeleteByIdAsync(Guid id);

    }
}
