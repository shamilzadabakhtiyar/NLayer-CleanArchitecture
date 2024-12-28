﻿using App.Repositories.Products;
using FluentValidation;

namespace App.Services.Products.Update
{
    public class UpdateProductRequestValidator: AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductRequestValidator(IProductRepository productRepository)
        {

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Must have a product name")
                .Length(3, 10).WithMessage("Product name must be between 3 and 10 characters");

            RuleFor(x => x.Price)
               .GreaterThan(0).WithMessage("Product price must be greater than 0");

            RuleFor(x => x.Stock)
             .InclusiveBetween(1, 100).WithMessage("Product stock must be between 1 and 100");

            RuleFor(x => x.CategoryId)
              .GreaterThan(0).WithMessage("Must have a product category");
        }
    }
}