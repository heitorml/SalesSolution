using Dto.Address;
using FluentValidation;

namespace Application.Validators
{
    public class AddressDtoValidator : AbstractValidator<AddressDto>
    {
        public AddressDtoValidator()
        {
            RuleFor(c => c.Street)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(40);

            RuleFor(c => c.ZipCode)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(40);

            RuleFor(c => c.City)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(40);

            RuleFor(c => c.Number)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(40); 
        }
    }
}
