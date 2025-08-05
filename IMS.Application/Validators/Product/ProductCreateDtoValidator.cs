using FluentValidation;
using Inventory_Management_System.Dtos.Products;

namespace Inventory_Management_System.Validators.Product
{
    public class ProductCreateDtoValidator : AbstractValidator<CreateProductDto>
    {
        public ProductCreateDtoValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Product Name is Required")
                .Length(3, 30).WithMessage("Product Length is Must be 3 to 30");

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("Product Price Must be greater than 0");

            RuleFor(p => p.BrandId)
                .GreaterThan(0).WithMessage("Brand Id Must be greater than 0");

            RuleFor(p => p.CategoryId)
                .GreaterThan(0).WithMessage("Category must be greater than 0");
        }
    }
}
