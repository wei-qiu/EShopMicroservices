﻿using Basket.API.Data;
using Basket.API.Models;
using BuildingBlocks.CQRS;

namespace Basket.API.Basket.GetBasket
{
	public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;

	public record GetBasketResult(ShoppingCart Cart);

	public class GetBasketQueryHandler(IBasketRepository repository) : IQueryHandler<GetBasketQuery, GetBasketResult>
	{
		public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
		{
			var basket = await repository.GetBasket(request.UserName);
			return new GetBasketResult(basket);
		}
	}
}
