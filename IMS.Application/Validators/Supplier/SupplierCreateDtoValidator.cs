using FluentValidation;
using IMS.Application.Dtos.Supplier;

namespace Inventory_Management_System.Validators.Supplier
{
    public class SupplierCreateDtoValidator : AbstractValidator<CreateSupplierDto>
    {
        public SupplierCreateDtoValidator()
        {
            RuleFor(s => s.Name)
                .NotEmpty().WithMessage("Supplier Name must not be Empty");
            RuleFor(s => s.Email)
                .EmailAddress().WithMessage("Inavlid Emial Address");

            RuleFor(s => s.Phone)
                .Matches(@"^\d{10,15}$").WithMessage("Phone Number Contains Only Digit and be 10 to 15 char long");
        }
    }
}
