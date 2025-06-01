using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Interfaces;
using ProductCatalog.Application.Interfaces.Repositories;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Infrastructure.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryRepository(AppDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Category>> GetAllAsync(bool includeDeleted = false)
        {
            var query = _context.Categories.AsQueryable();

            if (includeDeleted)
                query = query.IgnoreQueryFilters();

            return await query.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(Guid id, bool includeDeleted = false)
        {
            var query = _context.Categories.AsQueryable();

            if (includeDeleted)
                query = query.IgnoreQueryFilters();

            return await query.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Category category)
        {
            _context.Categories.Add(category);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                category.IsDeleted = true;
                _context.Categories.Update(category);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
