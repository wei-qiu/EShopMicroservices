using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;

namespace BuildingBlocks.Behaviors
{
	public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
		: IPipelineBehavior<TRequest, TResponse>
		where TRequest : ICommand<TResponse>  //this validation behavior works only with ICammand interface
	{
		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			var context = new ValidationContext<TRequest>(request);

			var validatorResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));

			var failures = validatorResults
				.Where(vr => vr.Errors.Any())
				.SelectMany(vr => vr.Errors)
				.ToList();

			if (failures.Any())
			{
				throw new ValidationException(failures);
			}

			return await next();
		}
	}
}
