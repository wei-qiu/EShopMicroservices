using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Extensions
{
	public static class InitialData
	{
		public static IEnumerable<Customer> Customers =>
			new List<Customer>
			{
				Customer.Create(CustomerId.Of(new Guid("2ff7845a-8d25-4a65-b8a9-c0826306b9a3")), "david", "dtest@gmail.com"),
				Customer.Create(CustomerId.Of(new Guid("16ba592e-215e-4541-87d4-79118703255a")), "peter", "peter@gmail.com")
			};

		public static IEnumerable<Product> Products =>
			new List<Product>
			{
				Product.Create(ProductId.Of(new Guid("b1c2d3e4-f5a6-7b8c-9d0e-f1a2b3c4d5e6")), "IPhone X",  500m),
				Product.Create(ProductId.Of(new Guid("f1a2b3c4-d5e6-7b8c-9d0e-b1c2d3e4f5a6")), "Samsung",  400m)
			};

		public static IEnumerable<Order> OrdersWithItems
		{
			get
			{
				var address1 = Address.Of("david", "test", "dtest@gmail.com", "123 Main St", "USA", "CA", "90001");
				var address2 = Address.Of("peter", "grifin", "peter@gmail.com", "456 Elm St", "USA", "NY", "10001");

				var payment1 = Payment.Of("david", "5555555555555555", "123", "12/25", 1);
				var payment2 = Payment.Of("peter", "6666666666666666", "456", "11/24", 2);

				var order1 = Order.Create(
					OrderId.Of(Guid.NewGuid()),
					CustomerId.Of(new Guid("2ff7845a-8d25-4a65-b8a9-c0826306b9a3")),
					OrderName.Of("Ord1"),
					shippingAddress: address1,
					billingAddress: address1,
					payment1);

				order1.Add(ProductId.Of(new Guid("b1c2d3e4-f5a6-7b8c-9d0e-f1a2b3c4d5e6")), 2, 500m);
				order1.Add(ProductId.Of(new Guid("f1a2b3c4-d5e6-7b8c-9d0e-b1c2d3e4f5a6")), 1, 400m);

				var order2 = Order.Create(
					OrderId.Of(Guid.NewGuid()),
					CustomerId.Of(new Guid("16ba592e-215e-4541-87d4-79118703255a")),
					OrderName.Of("Ord2"),
					shippingAddress: address2,
					billingAddress: address2,
					payment2);

				order2.Add(ProductId.Of(new Guid("b1c2d3e4-f5a6-7b8c-9d0e-f1a2b3c4d5e6")), 1, 500m);
				order2.Add(ProductId.Of(new Guid("f1a2b3c4-d5e6-7b8c-9d0e-b1c2d3e4f5a6")), 3, 400m);

				return new List<Order> { order1, order2 };
			}
		}

	}
}
