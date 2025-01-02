using App.Application.Contracts.Persistence;
using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Categories
{
    public class CategoryRepository(AppDbContext context) : GenericRepository<Category, int>(context), ICategoryRepository
    {
        public Task<Category?> GetCategoryWithProductsAsync(int id) =>
            Context.Categories.Include(c => c.Products).FirstOrDefaultAsync(x => x.Id == id);

        public Task<List<Category>> GetCategoryWithProductsAsync() =>
            Context.Categories.Include(c => c.Products).ToListAsync();
    }
}
