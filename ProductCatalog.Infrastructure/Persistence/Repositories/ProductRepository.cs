using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Interfaces;
using ProductCatalog.Application.Interfaces.Repositories;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public ProductRepository(AppDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Product>> GetAllAsync(bool includeDeleted = false)
        {
            var query = _context.Products.Include(p => p.Category).AsQueryable();

            if (includeDeleted)
                query = query.IgnoreQueryFilters();

            return await query.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(Guid id, bool includeDeleted = false)
        {
            var query = _context.Products.Include(p => p.Category).AsQueryable();

            if (includeDeleted)
                query = query.IgnoreQueryFilters();

            return await query.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                product.IsDeleted = true;
                _context.Products.Update(product);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
