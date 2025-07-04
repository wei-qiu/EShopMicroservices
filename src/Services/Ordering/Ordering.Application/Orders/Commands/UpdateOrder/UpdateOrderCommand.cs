﻿using FluentValidation;
using Ordering.Application.Dtos;

namespace Ordering.Application.Orders.Commands.UpdateOrder
{
	public record UpdateOrderCommand(OrderDto Order) : ICommand<UpdateOrderResult>;

	public record UpdateOrderResult(
		bool IsSuccess
	);

	public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
	{
		public UpdateOrderCommandValidator()
		{
			RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("Order name is required");
			RuleFor(x => x.Order.CustomerId).NotNull().WithMessage("CustomerId is required");
			RuleFor(x => x.Order.OrderItems).NotEmpty().WithMessage("OrderItems should not be empty");
		}
	}

}
