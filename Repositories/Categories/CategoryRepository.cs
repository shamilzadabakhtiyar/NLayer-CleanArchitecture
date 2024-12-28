using App.Repositories.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace App.Repositories.Categories
{
    public class CategoryRepository(AppDbContext context) : GenericRepository<Category, int>(context), ICategoryRepository
    {
        public Task<Category?> GetCategoryWithProducts(int id) =>
            Context.Categories.Include(c => c.Products).FirstOrDefaultAsync(x => x.Id == id);

        public IQueryable<Category?> GetCategoryWithProducts() =>
            Context.Categories.Include(c => c.Products);
    }
}
