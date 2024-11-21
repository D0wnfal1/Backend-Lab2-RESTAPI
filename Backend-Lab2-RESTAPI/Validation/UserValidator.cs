using Backend_Lab2_RESTAPI.Models;
using FluentValidation;

namespace Backend_Lab2_RESTAPI.Validation
{
	public class UserValidator : AbstractValidator<User>
	{
		public UserValidator()
		{
			RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.")
								 .Length(3, 50).WithMessage("Name must be between 3 and 50 characters.");
			RuleFor(x => x.DefaultCurrencyId).GreaterThan(0).WithMessage("DefaultCurrencyId must be greater than zero.");
		}
	}
}
