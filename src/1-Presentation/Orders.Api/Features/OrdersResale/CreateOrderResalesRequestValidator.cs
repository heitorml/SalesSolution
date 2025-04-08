using FluentValidation;

namespace Orders.Api.Features.OrdersResale
{
    public class CreateOrderResalesRequestValidator : AbstractValidator<CreateOrderResalesResquest>
    {
        public CreateOrderResalesRequestValidator()
        {

            RuleFor(c => c.ResaleId)
               .NotEmpty()
               .NotNull();

            RuleFor(c => c.Items)
                .NotEmpty()
                .NotNull()
                .ForEach(item =>
                {
                    item.SetValidator(new OrderItemsRequestDtoValidator());
                });
        }
    }

    public class OrderItemsRequestDtoValidator : AbstractValidator<OrderItemsRequestDto>
    {
        public OrderItemsRequestDtoValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(40);

            RuleFor(c => c.Price)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0);

            RuleFor(c => c.Quantity)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0);

            RuleFor(c => c.Description)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(40);
        }
    }

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
