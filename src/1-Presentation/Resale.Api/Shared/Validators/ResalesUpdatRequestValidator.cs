using FluentValidation;
using Resales.Api.Shared.Extensions;
using Resales.Api.Shared.Requests;

namespace Resales.Api.Shared.Validator
{
    public class ResalesUpdatRequestValidator : AbstractValidator<ResaleUpdateRequest>
    {
        public ResalesUpdatRequestValidator()
        {
            RuleFor(c => c.Id)
               .NotNull()
               .NotEmpty();

            RuleFor(c => c.Cnpj)
                .NotNull()
                .NotEmpty()
                .Must(ValidarCNPJ);

            RuleFor(c => c.Name)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(70);

            RuleFor(c => c.FantasyName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(100);

            RuleFor(c => c.Email)
                .NotNull()
                .NotEmpty();

            RuleFor(c => c.Phone)
                .NotNull()
                .NotEmpty();

            RuleFor(c => c.ContactName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(50);


            RuleFor(c => c.Addresses)
              .NotEmpty()
              .NotNull()
              .ForEach(item =>
              {
                  item.SetValidator(new AddressDtoValidator());
              });
        }

        private bool ValidarCNPJ(string cnpj) => cnpj.ValideCnpjString();
    }
}
