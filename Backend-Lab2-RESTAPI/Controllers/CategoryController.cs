using Backend_Lab1_RESTAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Backend_Lab1_RESTAPI.Controllers
{
	[Route("category")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private static List<Category> _categories = new List<Category>
		{
			new Category { Id = 1, CategoryName = "Appetizers" },
			new Category { Id = 2, CategoryName = "Main Courses" },
			new Category { Id = 3, CategoryName = "Drinks" }
		};

		[HttpGet]
		public IActionResult GetCategories()
		{
			return Ok(_categories);
		}

		[HttpPost]
		public IActionResult CreateCategory(string categoryName)
		{
			if (categoryName == null || string.IsNullOrEmpty(categoryName))
			{
				return BadRequest("Invalid category name.");
			}
			Category newCategory = new Category()
			{
				CategoryName = categoryName
			};
			newCategory.Id = _categories.Max(c => c.Id) + 1;

			_categories.Add(newCategory);
			return CreatedAtAction(nameof(GetCategories), new { id = newCategory.Id }, newCategory);
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteCategory(int id)
		{
			var category = _categories.FirstOrDefault(c => c.Id == id);
			if (category == null)
			{
				return NotFound($"Category with ID {id} not found.");
			}

			_categories.Remove(category);
			return NoContent();
		}
	}
}