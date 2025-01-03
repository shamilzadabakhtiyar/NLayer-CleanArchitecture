using App.Application.Contracts.Caching;
using App.Application.Contracts.Persistence;
using App.Application.Features.Products.Create;
using App.Application.Features.Products.Dto;
using App.Application.Features.Products.Update;
using App.Application.Features.Products.UpdateStock;
using App.Domain.Entities;
using App.Services.Products;
using AutoMapper;
using System.Net;

namespace App.Application.Features.Products
{
    public class ProductService(
        IProductRepository productRepository, 
        IUnitOfWork unitOfWork, 
        IMapper mapper,
        ICacheService cacheService) : IProductService
    {
        private const string ProductListCacheKey = "ProductListCacheKey";
        public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count)
        {
            var products = await productRepository.GetTopPriceProductsAsync(count);
            //var productsDto = products
            //    .Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock))
            //    .ToList();
            var productsDto = mapper.Map<List<ProductDto>>(products);

            return ServiceResult<List<ProductDto>>.Success(productsDto);

        }
        public async Task<ServiceResult<List<ProductDto>>> GetAllAsync()
        {
            var productsAsCached = await cacheService.GetAsync<List<ProductDto>>(ProductListCacheKey);
            if (productsAsCached is not null) return ServiceResult<List<ProductDto>>.Success(productsAsCached);

            var products = await productRepository.GetAllAsync();
            //var productsDto = await products
            //    .Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock))
            //    .ToListAsync();
            var productsDto = mapper.Map<List<ProductDto>>(products);
            await cacheService.AddAsync(ProductListCacheKey, productsDto, TimeSpan.FromMinutes(1));
            return ServiceResult<List<ProductDto>>.Success(productsDto);

        }
        public async Task<ServiceResult<List<ProductDto>>> GetPagedAllAsync(int pageNumber, int pageSize)
        {
            var products = await productRepository.GetAllPagedAsync(pageNumber, pageSize);
            var productsDto = mapper.Map<List<ProductDto>>(products);
            return ServiceResult<List<ProductDto>>.Success(productsDto);
        }
        public async Task<ServiceResult<ProductDto>> GetByIdAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);
            var productDto = mapper.Map<ProductDto>(product);
            return ServiceResult<ProductDto>.Success(productDto);
        }
        public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
        {
            if (await productRepository.AnyAsync(x => x.Name == request.Name))
                return ServiceResult<CreateProductResponse>.Fail("Product name must be unique");

            //var product = new Product
            //{
            //    Name = request.Name,
            //    Price = request.Price,
            //    Stock = request.Stock
            //};
            var product = mapper.Map<Product>(request);
            await productRepository.AddAsync(product);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult<CreateProductResponse>.SuccessAsCreated(new CreateProductResponse(product.Id), $"api/products/{product.Id}");
        }
        public async Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest request)
        {
            if (await productRepository.AnyAsync(x => x.Name == request.Name && x.Id != id))
                return ServiceResult.Fail("Product name must be unique");

            var product = mapper.Map<Product>(request);
            product.Id = id;
            productRepository.Update(product!);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }
        public async Task<ServiceResult> UpdateStockAsync(UpdateProductStockRequest request)
        {
            var product = await productRepository.GetByIdAsync(request.ProductId);
            product!.Stock = request.Quantity;
            productRepository.Update(product);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }
        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);
            productRepository.Delete(product!);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }
    }
}