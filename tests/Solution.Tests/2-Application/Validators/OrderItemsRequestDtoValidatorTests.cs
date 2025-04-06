using Application.Validators;
using Dto.OrderItems.Requests;
using FluentValidation.TestHelper;

namespace Solution.Tests._2_Application.Validators
{
    public class OrderItemsRequestDtoValidatorTests
    {
        private readonly OrderItemsRequestDtoValidator _validator;

        public OrderItemsRequestDtoValidatorTests()
        {
            _validator = new OrderItemsRequestDtoValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Fields_Are_Empty()
        {
            var model = new OrderItemsRequestDto
            {
                Name = "",
                Price = 0,
                Quantity = 0,
                Description = ""
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Name);
            result.ShouldHaveValidationErrorFor(x => x.Price);
            result.ShouldHaveValidationErrorFor(x => x.Quantity);
            result.ShouldHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public void Should_Have_Error_When_Fields_Are_Too_Short()
        {
            var model = new OrderItemsRequestDto
            {
                Name = "AB",
                Price = 10,
                Quantity = 1,
                Description = "AB"
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Name);
            result.ShouldHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public void Should_Have_Error_When_Fields_Are_Too_Long()
        {
            var tooLong = new string('X', 41);

            var model = new OrderItemsRequestDto
            {
                Name = tooLong,
                Price = 10,
                Quantity = 1,
                Description = tooLong
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Name);
            result.ShouldHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public void Should_Have_Error_When_Values_Are_Zero_Or_Less()
        {
            var model = new OrderItemsRequestDto
            {
                Name = "Produto",
                Price = 0,
                Quantity = 0,
                Description = "Descrição válida"
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Price);
            result.ShouldHaveValidationErrorFor(x => x.Quantity);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Fields_Are_Valid()
        {
            var model = new OrderItemsRequestDto
            {
                Name = "Produto 01",
                Price = 50,
                Quantity = 5,
                Description = "Produto de qualidade"
            };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
