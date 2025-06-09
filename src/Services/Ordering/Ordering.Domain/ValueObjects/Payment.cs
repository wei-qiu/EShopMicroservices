
namespace Ordering.Domain.ValueObjects
{
	public record Payment
	{
		public string CardNumber { get; private set; } = default!;
		public string CardName { get; private set; } = default!;
		public string CVV { get; private set; } = default!;
		public string Expiration { get; } = default!;
		public int PaymentMethod { get; } = default!;

		// Parameterless constructor for EF Core
		private Payment() { }

		private Payment(string cardNumber, string cardName, string cvv, string expiration, int paymentMethod)
		{
			CardNumber = cardNumber;
			CardName = cardName;
			CVV = cvv;
			Expiration = expiration;
			PaymentMethod = paymentMethod;
		}

		public static Payment Of(string cardNumber, string cardName, string cvv, string expiration, int paymentMethod)
		{
			ArgumentNullException.ThrowIfNull(cardNumber);
			ArgumentNullException.ThrowIfNull(cardName);
			ArgumentNullException.ThrowIfNull(cvv);
			ArgumentNullException.ThrowIfNull(expiration);
			ArgumentNullException.ThrowIfNull(paymentMethod);
			if (string.IsNullOrWhiteSpace(cardNumber))
				throw new DomainException("CardNumber cannot be empty");
			if (string.IsNullOrWhiteSpace(cardName))
				throw new DomainException("CardName cannot be empty");
			if (string.IsNullOrWhiteSpace(cvv))
				throw new DomainException("CVV cannot be empty");
			if (string.IsNullOrWhiteSpace(expiration))
				throw new DomainException("Expiration cannot be empty");
			return new Payment(cardNumber, cardName, cvv, expiration, paymentMethod);
		}
	}
}
