using Backend_Lab2_RESTAPI.Models;
using FluentValidation;

namespace Backend_Lab2_RESTAPI.Validation
{
	public class CategoryValidator : AbstractValidator<Category>
	{
		public CategoryValidator()
		{
			RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Category name is required.")
										 .Length(3, 100).WithMessage("Category name must be between 3 and 100 characters.");
		}
	}
}
