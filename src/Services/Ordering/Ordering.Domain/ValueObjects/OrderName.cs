using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObjects
{
	public record OrderName
	{
		private static int defaultLength = 5;
		public string Value { get; }
		private OrderName(string value) => Value = value;
		public static OrderName Of(string value)
		{
			ArgumentNullException.ThrowIfNull(value);
			if (string.IsNullOrWhiteSpace(value))
				throw new DomainException("OrderName cannot be empty");
			//if (value.Length > defaultLength)
			//	throw new DomainException("OrderName cannot be longer than 5 characters");
			return new OrderName(value);
		}
	}
}
