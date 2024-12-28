using App.Repositories.Products;
using Microsoft.EntityFrameworkCore.Query;

namespace App.Repositories.Categories
{
    public interface ICategoryRepository: IGenericRepository<Category, int>
    {
        Task<Category?> GetCategoryWithProducts(int id);
        IQueryable<Category?> GetCategoryWithProducts();
    }
}
