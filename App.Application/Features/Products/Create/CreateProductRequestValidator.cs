using App.Application.Contracts.Persistence;
using FluentValidation;

namespace App.Application.Features.Products.Create
{
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
        private readonly IProductRepository _productRepository;
        public CreateProductRequestValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Must have a product name")
                .Length(3, 10).WithMessage("Product name must be between 3 and 10 characters");
            //.Must(UniqueProductName).WithMessage("Product name must be unique");

            RuleFor(x => x.Price)
               .GreaterThan(0).WithMessage("Product price must be greater than 0");

            RuleFor(x => x.Stock)
             .InclusiveBetween(1, 100).WithMessage("Product stock must be between 1 and 100");

            RuleFor(x => x.CategoryId)
              .GreaterThan(0).WithMessage("Must have a product category");
        }

        private bool UniqueProductName(string productName)
        {
            return !_productRepository
                .Where(x => x.Name == productName)
                .Any();
        }
    }
}
