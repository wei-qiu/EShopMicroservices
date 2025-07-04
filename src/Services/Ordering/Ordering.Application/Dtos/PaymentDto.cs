using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Dtos
{
	public record PaymentDto(
		string CardNumber,
		string CardName,
		string Expiration,
		string Cvv,
		int PaymentMethod
	);

}
