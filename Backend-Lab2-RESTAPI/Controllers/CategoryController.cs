using Backend_Lab2_RESTAPI.Data;
using Backend_Lab2_RESTAPI.Models;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Backend_Lab2_RESTAPI.Validation;
using Microsoft.AspNetCore.Authorization;

namespace Backend_Lab2_RESTAPI.Controllers
{
	[Route("category")]
	[ApiController]
	[Authorize]
	public class CategoryController : ControllerBase
	{
		private readonly AppDbContext _dbContext;
		private readonly IValidator<Category> _categoryValidator;

		public CategoryController(AppDbContext dbContext, IValidator<Category> categoryValidator)
		{
			_dbContext = dbContext;
			_categoryValidator = categoryValidator;
		}

		[HttpGet]
		public IActionResult GetCategories()
		{
			return Ok(_dbContext.Categories);
		}

		[HttpPost]
		public IActionResult CreateCategory([FromBody] Category category)
		{
			var validationResult = _categoryValidator.Validate(category);
			if (!validationResult.IsValid)
			{
				return BadRequest(validationResult.Errors);
			}

			category.Id = _dbContext.Categories.Any() ? _dbContext.Categories.Max(c => c.Id) + 1 : 1;

			_dbContext.Categories.Add(category);
			_dbContext.SaveChanges();
			return CreatedAtAction(nameof(GetCategories), new { id = category.Id }, category);
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteCategory(int id)
		{
			var category = _dbContext.Categories.FirstOrDefault(c => c.Id == id);
			if (category == null)
			{
				return NotFound($"Category with ID {id} not found.");
			}

			_dbContext.Categories.Remove(category);
			_dbContext.SaveChanges();
			return NoContent();
		}
	}
}
