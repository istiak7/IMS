using FluentValidation;
using IMS.Application.Dtos.WarehouseInfo;

namespace Inventory_Management_System.Validators.Warehouse
{
    public class WarehouseCreateDtoValidator : AbstractValidator<CreateWarehouseDto>
    {
        public WarehouseCreateDtoValidator()
        {
            RuleFor(w => w.Name)
               .NotEmpty().WithMessage("Warehouse Name must not be Empty");

            RuleFor(w => w.Phone)
                .Matches(@"^\d{10,15}$").WithMessage("Phone Number Contains Only Digit and be 10 to 15 char long");

            RuleFor(w => w.Location)
                .MaximumLength(20).WithMessage("Maximum length is 20")
                .MinimumLength(3).WithMessage("Minimum length is 3");
        }
    }
}
