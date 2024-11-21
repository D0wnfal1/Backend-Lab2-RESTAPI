using Backend_Lab2_RESTAPI.Data;
using Backend_Lab2_RESTAPI.Models;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Backend_Lab2_RESTAPI.Validation;

namespace Backend_Lab2_RESTAPI.Controllers
{
	[Route("user")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly AppDbContext _dbContext;
		private readonly IValidator<User> _userValidator;

		public UserController(AppDbContext dbContext, IValidator<User> userValidator)
		{
			_dbContext = dbContext;
			_userValidator = userValidator;
		}

		[HttpGet]
		public IActionResult GetUsers()
		{
			return Ok(_dbContext.Users);
		}

		[HttpGet("{id}")]
		public IActionResult GetUserById(int id)
		{
			var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);
			if (user == null)
			{
				return NotFound($"User with ID {id} not found.");
			}
			return Ok(user);
		}

		[HttpPost]
		public IActionResult CreateUser([FromBody] User user)
		{
			var validationResult = _userValidator.Validate(user);
			if (!validationResult.IsValid)
			{
				return BadRequest(validationResult.Errors);
			}

			user.Id = _dbContext.Users.Any() ? _dbContext.Users.Max(c => c.Id) + 1 : 1;

			_dbContext.Users.Add(user);
			_dbContext.SaveChanges();
			return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteUser(int id)
		{
			var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);
			if (user == null)
			{
				return NotFound($"User with ID {id} not found.");
			}

			_dbContext.Users.Remove(user);
			_dbContext.SaveChanges();
			return NoContent();
		}
	}
}
