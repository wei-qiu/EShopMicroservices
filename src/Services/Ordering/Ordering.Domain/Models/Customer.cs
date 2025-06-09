using Ordering.Domain.Abstractions;


namespace Ordering.Domain.Models
{
	public class Customer : Entity<CustomerId>
	{
		public string Name { get; private set; } = default!;
		public string Email { get; private set; } = default!;

		public static Customer Create(CustomerId customerId, string name, string email)
		{
			ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
			ArgumentException.ThrowIfNullOrEmpty(email, nameof(email));

			return new Customer
			{
				Id = customerId,
				Name = name,
				Email = email
			};
		}
	}

}
