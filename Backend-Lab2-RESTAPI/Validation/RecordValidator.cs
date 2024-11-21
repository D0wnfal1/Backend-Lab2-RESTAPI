using Backend_Lab2_RESTAPI.Models;
using FluentValidation;

namespace Backend_Lab2_RESTAPI.Validation
{
	public class RecordValidator : AbstractValidator<Record>
	{
		public RecordValidator()
		{
			RuleFor(x => x.UserId).GreaterThan(0).WithMessage("UserId must be greater than zero.");
			RuleFor(x => x.CategoryId).GreaterThan(0).WithMessage("CategoryId must be greater than zero.");
			RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount must be greater than zero.");
			RuleFor(x => x.CreateTime).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("CreateTime cannot be in the future.");
		}
	}
}
