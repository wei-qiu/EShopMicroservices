﻿using Basket.API.Exceptions;
using Basket.API.Models;
using Marten;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Basket.API.Data
{
	public class BasketRepository(IDocumentSession session) : IBasketRepository
	{
		public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
		{
			session.Delete<ShoppingCart>(userName);
			await session.SaveChangesAsync();
			return true;
		}

		public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
		{
			var basket = await session.LoadAsync<ShoppingCart>(userName, cancellationToken);
			return basket == null ? throw new BasketNotFoundException(userName) : basket;
		}

		public async Task<ShoppingCart> StoreBasket(ShoppingCart cart, CancellationToken cancellationToken = default)
		{
			session.Store(cart);
			await session.SaveChangesAsync();
			return cart;
		}
	}
}
