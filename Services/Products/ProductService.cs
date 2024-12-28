using App.Repositories;
using App.Repositories.Products;
using App.Services.Products.Create;
using App.Services.Products.Update;
using App.Services.Products.UpdateStock;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Products
{
    public class ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
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
            var products = await productRepository.GetAll().ToListAsync();
            //var productsDto = await products
            //    .Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock))
            //    .ToListAsync();
            var productsDto = mapper.Map<List<ProductDto>>(products);

            return ServiceResult<List<ProductDto>>.Success(productsDto);

        }
        public async Task<ServiceResult<List<ProductDto>>> GetPagedAllAsync(int pageNumber, int pageSize)
        {
            var products = await productRepository.GetAll()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
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
            if (await productRepository
                .Where(x => x.Name == request.Name)
                .AnyAsync())
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
            if (await productRepository
               .Where(x => x.Name == request.Name && x.Id != id)
               .AnyAsync())
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