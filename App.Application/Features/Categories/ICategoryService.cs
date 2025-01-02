using App.Application.Features.Categories.Create;
using App.Application.Features.Categories.Dto;
using App.Application.Features.Categories.Update;

namespace App.Application.Features.Categories
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