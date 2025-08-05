using FluentValidation;
using IMS.Application.Dtos.Customer;

namespace Inventory_Management_System.Validators.Customer
{
    public class CustomerCreateDtoValidator : AbstractValidator<CreateCustomerDto>
    {
        public CustomerCreateDtoValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Customer name mustnot be empty");

            RuleFor(c => c.Email)
                .EmailAddress().WithMessage("Invalid Email");
            RuleFor(c => c.Phone)
                .Matches(@"^\d{10,15}").WithMessage("phone number only contains digit and length 10 to 15");
            RuleFor(c => c.Address)
                .NotEmpty().WithMessage("Address mustnot be Empty");
        }
    }
}
