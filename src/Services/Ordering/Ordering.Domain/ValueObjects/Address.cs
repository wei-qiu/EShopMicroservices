

namespace Ordering.Domain.ValueObjects
{
	public record Address
	{
		public string FirstName { get; private set; } = default!;
		public string LastName { get; private set; } = default!;
		public string? EmailAddress { get; private set; } = default!;
		public string AddressLine { get; private set; } = default!;
		public string Country { get; private set; } = default!;
		public string State { get; private set; } = default!;
		public string ZipCode { get; private set; } = default!;

		private Address(string firstName, string lastName, string? emailAddress, string addressLine, string country, string state, string zipCode)
		{
			FirstName = firstName;
			LastName = lastName;
			EmailAddress = emailAddress;
			AddressLine = addressLine;
			Country = country;
			State = state;
			ZipCode = zipCode;
		}

		public static Address Of(string firstName, string lastName, string? emailAddress, string addressLine, string country, string state, string zipCode)
		{
			ArgumentNullException.ThrowIfNull(firstName);
			ArgumentNullException.ThrowIfNull(lastName);
			ArgumentNullException.ThrowIfNull(emailAddress);
			ArgumentNullException.ThrowIfNull(addressLine);
			ArgumentNullException.ThrowIfNull(country);
			ArgumentNullException.ThrowIfNull(state);
			ArgumentNullException.ThrowIfNull(zipCode);
			if (string.IsNullOrWhiteSpace(firstName))
				throw new DomainException("FirstName cannot be empty");
			if (string.IsNullOrWhiteSpace(lastName))
				throw new DomainException("LastName cannot be empty");
			if (string.IsNullOrWhiteSpace(addressLine))
				throw new DomainException("AddressLine cannot be empty");
			if (string.IsNullOrWhiteSpace(country))
				throw new DomainException("Country cannot be empty");
			if (string.IsNullOrWhiteSpace(state))
				throw new DomainException("State cannot be empty");
			if (string.IsNullOrWhiteSpace(zipCode))
				throw new DomainException("ZipCode cannot be empty");
			return new Address(firstName, lastName, emailAddress, addressLine, country, state, zipCode);
		}
	}
}
