using Application.Validators;
using Dto.Address;
using FluentValidation.TestHelper;

namespace Solution.Tests._2_Application.Validators
{
    public class AddressDtoValidatorTests
    {
        private readonly AddressDtoValidator _validator;

        public AddressDtoValidatorTests()
        {
            _validator = new AddressDtoValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Fields_Are_Empty()
        {
            var model = new AddressDto
            {
                Street = "",
                ZipCode = "",
                City = "",
                Number = ""
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Street);
            result.ShouldHaveValidationErrorFor(x => x.ZipCode);
            result.ShouldHaveValidationErrorFor(x => x.City);
            result.ShouldHaveValidationErrorFor(x => x.Number);
        }

        [Fact]
        public void Should_Have_Error_When_Fields_Are_Too_Short()
        {
            var model = new AddressDto
            {
                Street = "St",
                ZipCode = "12",
                City = "NY",
                Number = "1"
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Street);
            result.ShouldHaveValidationErrorFor(x => x.ZipCode);
            result.ShouldHaveValidationErrorFor(x => x.City);
            result.ShouldHaveValidationErrorFor(x => x.Number);
        }

        [Fact]
        public void Should_Have_Error_When_Fields_Are_Too_Long()
        {
            var tooLong = new string('A', 41);

            var model = new AddressDto
            {
                Street = tooLong,
                ZipCode = tooLong,
                City = tooLong,
                Number = tooLong
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Street);
            result.ShouldHaveValidationErrorFor(x => x.ZipCode);
            result.ShouldHaveValidationErrorFor(x => x.City);
            result.ShouldHaveValidationErrorFor(x => x.Number);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Fields_Are_Valid()
        {
            var model = new AddressDto
            {
                Street = "Main Street",
                ZipCode = "12345-678",
                City = "New York",
                Number = "123"
            };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
