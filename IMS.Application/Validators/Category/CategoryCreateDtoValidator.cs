using FluentValidation;
using IMS.Application.Dtos.Brand;

namespace Inventory_Management_System.Validators.Category
{
    public class CategoryCreateDtoValidator : AbstractValidator<CreateBrandDto>
    {
        public CategoryCreateDtoValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Category Name Cannot be Empty");
            RuleFor(c => c.Description)
                .MaximumLength(200).WithMessage("Description Length must not greater than 200")
                .MinimumLength(10).WithMessage("Description length must be grater than 9");
        }
    }
}
