using Dto.OrderItems.Requests;
using FluentValidation;

namespace Application.Validators
{
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
}
