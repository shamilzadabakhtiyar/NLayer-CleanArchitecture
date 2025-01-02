using App.Application;
using App.Application.Contracts.Persistence;
using App.Application.Features.Categories;
using App.Application.Features.Categories.Create;
using App.Application.Features.Categories.Dto;
using App.Application.Features.Categories.Update;
using App.Domain.Entities;
using AutoMapper;
using System.Net;

namespace App.Services.Categories
{
    public class CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper) : ICategoryService
    {
        public async Task<ServiceResult<CreateCategoryResponse>> CreateAsync(CreateCategoryRequest request)
        {
            if (await categoryRepository.AnyAsync(x => x.Name == request.Name))
                return ServiceResult<CreateCategoryResponse>.Fail("Name must be unique");

            var category = mapper.Map<Category>(request);
            await categoryRepository.AddAsync(category);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult<CreateCategoryResponse>.SuccessAsCreated(new CreateCategoryResponse(category.Id), $"api/categories/{category.Id}");
        }

        public async Task<ServiceResult> UpdateAsync(int id, UpdateCategoryRequest request)
        {
            if (await categoryRepository.AnyAsync(x => x.Name == request.Name && x.Id != id))
                return ServiceResult.Fail("Name must be unique");

            var category = mapper.Map<Category>(request);
            category.Id = id;
            categoryRepository.Update(category);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            categoryRepository.Delete(category!);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult<List<CategoryDto>>> GetAllAsync()
        {
            var categories = await categoryRepository.GetAllAsync();
            var categoriesDto = mapper.Map<List<CategoryDto>>(categories);
            return ServiceResult<List<CategoryDto>>.Success(categoriesDto);
        }

        public async Task<ServiceResult<CategoryDto>> GetByIdAsync(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            var productDto = mapper.Map<CategoryDto>(category);
            return ServiceResult<CategoryDto>.Success(productDto);
        }
        public async Task<ServiceResult<CategoryWithproductsDto>> GetCategoryWithProductsAsync(int id)
        {
            var category = await categoryRepository.GetCategoryWithProductsAsync(id);
            var productDto = mapper.Map<CategoryWithproductsDto>(category);
            return ServiceResult<CategoryWithproductsDto>.Success(productDto);
        }

        public async Task<ServiceResult<List<CategoryWithproductsDto>>> GetCategoryWithProductsAsync()
        {
            var categories = await categoryRepository.GetCategoryWithProductsAsync();
            var categoriesDto = mapper.Map<List<CategoryWithproductsDto>>(categories);
            return ServiceResult<List<CategoryWithproductsDto>>.Success(categoriesDto);
        }
    }
}
