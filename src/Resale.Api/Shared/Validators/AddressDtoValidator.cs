using FluentValidation;
using Resales.Api.Shared.Requests;

namespace Resales.Api.Shared.Validator
{
    public class AddressDtoValidator : AbstractValidator<AddressRequest>
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
