using App.Services.Categories.Create;
using App.Services.Categories.Dto;
using App.Services.Categories.Update;

namespace App.Services.Categories
{
    public interface ICategoryService
    {
        Task<ServiceResult<CreateCategoryResponse>> CreateAsync(CreateCategoryRequest request);
        Task<ServiceResult> DeleteAsync(int id);
        Task<ServiceResult<List<CategoryDto>>> GetAllAsync();
        Task<ServiceResult<CategoryDto>> GetByIdAsync(int id);
        Task<ServiceResult<List<CategoryWithproductsDto>>> GetCategoryWithProductsAsync();
        Task<ServiceResult<CategoryWithproductsDto>> GetCategoryWithProductsAsync(int id);
        Task<ServiceResult> UpdateAsync(int id, UpdateCategoryRequest request);
    }
}