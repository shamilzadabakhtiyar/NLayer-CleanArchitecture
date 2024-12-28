﻿using App.Repositories.Products;
using App.Services.Filters;
using App.Services.Products;
using App.Services.Products.Create;
using App.Services.Products.Update;
using App.Services.Products.UpdateStock;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    public class ProductsController(IProductService productService) : CustomControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() => CreateActionResult(await productService.GetAllAsync());

        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        public async Task<IActionResult> GetPagedAll(int pageNumber, int pageSize) => CreateActionResult(await productService.GetPagedAllAsync(pageNumber, pageSize));

        //[HttpGet]
        //public async Task<IActionResult> GetTopPriceProducts(int count) => CreateActionResult(await productService.GetTopPriceProductsAsync(count));
        
        [ServiceFilter(typeof(NotFoundFilter<Product, int>))]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id) => CreateActionResult(await productService.GetByIdAsync(id));

        [ServiceFilter(typeof(NotFoundFilter<Product, int>))]
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest request) => CreateActionResult(await productService.CreateAsync(request));

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateProductRequest request) => CreateActionResult(await productService.UpdateAsync(id, request));

        [HttpPatch("stock")]
        public async Task<IActionResult> UpdateStock(UpdateProductStockRequest request) => CreateActionResult(await productService.UpdateStockAsync(request));

        [ServiceFilter(typeof(NotFoundFilter<Product, int>))]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id) => CreateActionResult(await productService.DeleteAsync(id));
    }
}
