using FluentValidation.TestHelper;
using Resales.Api.Shared.Validator;

namespace Solution.Tests._2_Application.Validators
{
    public class ResalesRequestValidatorTests
    {
        private readonly ResalesRequestValidator _validator;

        public ResalesRequestValidatorTests()
        {
            _validator = new ResalesRequestValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Cnpj_Is_Invalid()
        {
            var dto = GetValidDto();
            dto.Cnpj = "000000000"; // inválido

            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Cnpj);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Too_Short()
        {
            var dto = GetValidDto();
            dto.Name = "A";

            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Have_Error_When_FantasyName_Is_Null()
        {
            var dto = GetValidDto();
            dto.FantasyName = null;

            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.FantasyName);
        }

        [Fact]
        public void Should_Have_Error_When_Email_Is_Empty()
        {
            var dto = GetValidDto();
            dto.Email = "";

            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Should_Have_Error_When_Phone_Is_Null()
        {
            var dto = GetValidDto();
            dto.Phone = null;

            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Phone);
        }

        [Fact]
        public void Should_Have_Error_When_ContactName_Is_Short()
        {
            var dto = GetValidDto();
            dto.ContactName = "A";

            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.ContactName);
        }

        [Fact]
        public void Should_Have_Error_When_Address_Is_Null()
        {
            var dto = GetValidDto();
            dto.Address = null;

            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Address);
        }

        [Fact]
        public void Should_Have_ChildValidator_For_Address()
        {
            _validator.ShouldHaveChildValidator(x => x.Address, typeof(AddressDtoValidator));
        }

        [Fact]
        public void Should_Not_Have_Errors_When_Model_Is_Valid()
        {
            var dto = GetValidDto();

            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveAnyValidationErrors();
        }

        private ResalesRequest GetValidDto() => new ResalesRequest
        {
            Cnpj = "12345678000195", // use um CNPJ válido conforme a regra do método de extensão
            Name = "Loja Teste",
            FantasyName = "LT Teste",
            Email = "teste@loja.com",
            Phone = "11999999999",
            ContactName = "Fulano de Tal",
            Address = new AddressDto
            {
                Street = "Rua X",
                ZipCode = "12345678",
                City = "Cidade Y",
                Number = "123"
            }
        };
    }
}
