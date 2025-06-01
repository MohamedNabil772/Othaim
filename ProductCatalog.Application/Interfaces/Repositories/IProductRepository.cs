using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync(bool includeDeleted = false);      
        Task<Product?> GetByIdAsync(Guid id, bool includeDeleted = false);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Guid id);
    }
}
