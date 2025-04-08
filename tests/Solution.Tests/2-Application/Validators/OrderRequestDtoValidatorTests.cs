//using Application.Validators;
//using Dto.Address;
//using Dto.OrderItems.Requests;
//using Dto.Orders.Requests;
//using Dto.Resales.Requests;
//using FluentValidation.TestHelper;
//using Orders.Worker.Shared.Requests;

//namespace Solution.Tests._2_Application.Validators
//{
//    public class OrderRequestDtoValidatorTests
//    {
//        private readonly OrderRequestDtoValidator _validator;

//        public OrderRequestDtoValidatorTests()
//        {
//            _validator = new OrderRequestDtoValidator();
//        }

//        [Fact]
//        public void Should_Have_Error_When_ResaleId_Is_Empty()
//        {
//            var dto = new OrderRequestDto
//            {
//                ResaleId = "",
//                Resale = GetValidResale(),
//                Items = new List<OrderItemsRequestDto> { GetValidItem() }
//            };

//            var result = _validator.TestValidate(dto);
//            result.ShouldHaveValidationErrorFor(x => x.ResaleId);
//        }

//        [Fact]
//        public void Should_Have_Error_When_Resale_Is_Null()
//        {
//            var dto = new OrderRequestDto
//            {
//                ResaleId = "valid-id",
//                Resale = null,
//                Items = new List<OrderItemsRequestDto> { GetValidItem() }
//            };

//            var result = _validator.TestValidate(dto);
//            result.ShouldHaveValidationErrorFor(x => x.Resale);
//        }

//        [Fact]
//        public void Should_Have_Error_When_Resale_Is_Invalid()
//        {
//            var dto = new OrderRequestDto
//            {
//                ResaleId = "valid-id",
//                Resale = new ResalesRequestDto(), // inválido
//                Items = new List<OrderItemsRequestDto> { GetValidItem() }
//            };

//            var result = _validator.TestValidate(dto);
//            _validator.ShouldHaveChildValidator(x => x.Resale, typeof(ResalesRequestValidator));
//        }

//        [Fact]
//        public void Should_Have_Error_When_Items_Is_Null()
//        {
//            var dto = new OrderRequestDto
//            {
//                ResaleId = "valid-id",
//                Resale = GetValidResale(),
//                Items = null
//            };

//            var result = _validator.TestValidate(dto);
//            result.ShouldHaveValidationErrorFor(x => x.Items);
//        }

//        [Fact]
//        public void Should_Have_Error_When_Items_Has_Invalid_Item()
//        {
//            var dto = new OrderRequestDto
//            {
//                ResaleId = "valid-id",
//                Resale = GetValidResale(),
//                Items = new List<OrderItemsRequestDto> { new OrderItemsRequestDto() } // item inválido
//            };

//            var result = _validator.TestValidate(dto);
//            //  _validator.ShouldHaveChildValidator(x => x.Items, typeof(OrderItemsRequestDtoValidator));
//        }

//        [Fact]
//        public void Should_Not_Have_Errors_When_Model_Is_Valid()
//        {
//            var dto = new OrderRequestDto
//            {
//                ResaleId = "valid-id",
//                Resale = GetValidResale(),
//                Items = new List<OrderItemsRequestDto> { GetValidItem() }
//            };

//            var result = _validator.TestValidate(dto);
//            // result.ShouldNotHaveAnyValidationErrors();
//        }

//        private ResalesRequestDto GetValidResale() => new ResalesRequestDto
//        {
//            Name = "Loja ABC",
//            Cnpj = "12345678900000",
//            Address =
//                new Orders.Worker.Shared.Requests.AddressDto
//                {
//                    Street = "Rua Teste",
//                    ZipCode = "12345678",
//                    City = "Cidade",
//                    Number = "100"
//                }
//        };

//        private Orders.Worker.Shared.Requests.OrderItemsRequestDto GetValidItem() => new Orders.Worker.Shared.Requests.OrderItemsRequestDto
//        {
//            Name = "Produto Teste",
//            Description = "Descrição",
//            Price = 100,
//            Quantity = 10
//        };
//    }
//}
