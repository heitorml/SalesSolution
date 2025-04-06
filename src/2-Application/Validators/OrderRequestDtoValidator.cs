using Dto.Orders.Requests;
using FluentValidation;

namespace Application.Validators
{
    public class OrderRequestDtoValidator : AbstractValidator<OrderRequestDto>
    {
        public OrderRequestDtoValidator()
        {

            RuleFor(c => c.ResaleId)
               .NotEmpty()
               .NotNull();

            RuleFor(c => c.Resale)
                .NotEmpty()
                .NotNull()
                .SetValidator(new ResalesRequestValidator());

            RuleFor(c => c.Items)
                .NotEmpty()
                .NotNull()
                .ForEach(item =>
                {
                    item.SetValidator(new OrderItemsRequestDtoValidator());
                });
        }
    }
}
