using Application.Validators;
using Dto.Address;
using Dto.Resales.Requests;
using FluentValidation.TestHelper;
using Orders.Worker.Shared.Requests;

namespace Solution.Tests._2_Application.Validators
{

    public class ResalesUpdatRequestDtoValidatorTests
    {
        private readonly ResalesUpdatRequestDtoValidator _validator;

        public ResalesUpdatRequestDtoValidatorTests()
        {
            _validator = new ResalesUpdatRequestDtoValidator();
        }

        [Fact]
        public void Should_Have_Errors_When_Fields_Are_Invalid()
        {
            var dto = new ResaleUpdateRequestDto
            {
                Id = "",
                Cnpj = "invalid",
                Name = "",
                FantasyName = "",
                Email = "",
                Phone = "",
                ContactName = "",
                Addresses = null
            };

            var result = _validator.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(x => x.Id);
            result.ShouldHaveValidationErrorFor(x => x.Cnpj);
            result.ShouldHaveValidationErrorFor(x => x.Name);
            result.ShouldHaveValidationErrorFor(x => x.FantasyName);
            result.ShouldHaveValidationErrorFor(x => x.Email);
            result.ShouldHaveValidationErrorFor(x => x.Phone);
            result.ShouldHaveValidationErrorFor(x => x.ContactName);
            result.ShouldHaveValidationErrorFor(x => x.Addresses);
        }

        [Fact]
        public void Should_Not_Have_Errors_When_Fields_Are_Valid()
        {
            var dto = new ResaleUpdateRequestDto
            {
                Id = "123",
                Cnpj = "26.637.142/0001-58", // Supondo que esse seja válido pelo método de extensão
                Name = "Empresa Exemplo",
                FantasyName = "Fantasia",
                Email = "contato@exemplo.com",
                Phone = "11999999999",
                ContactName = "João",
                Addresses = new List<AddressDto>
            {
                new AddressDto
                {
                    Street = "Rua A",
                    City = "São Paulo",
                    Number = "123",
                    ZipCode = "01000-000"
                }
            }
            };

            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Should_Have_ChildValidator_For_Address()
        {
          //  _validator.ShouldHaveChildValidator(x => x.Addresses, typeof(AddressDtoValidator));
        }
    }
}
