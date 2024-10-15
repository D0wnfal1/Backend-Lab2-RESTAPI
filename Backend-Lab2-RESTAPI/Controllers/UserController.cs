using Backend_Lab1_RESTAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Backend_Lab1_RESTAPI.Controllers
{
	[Route("user")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private static List<User> _users = new List<User>
		{
			new User { Id = 1, Name = "JohnDoe" },
			new User { Id = 2, Name = "AdamSmith" }
		};

		[HttpGet()]
		public IActionResult GetUsers()
		{
			return Ok(_users);
		}

		[HttpGet("{id}")]
		public IActionResult GetUserById(int id)
		{
			var user = _users.FirstOrDefault(u => u.Id == id);
			if (user == null)
			{
				return NotFound($"User with ID {id} not found.");
			}
			return Ok(user);
		}

		[HttpPost]
		public IActionResult CreateUser(string userName)
		{
			if (userName == null || string.IsNullOrEmpty(userName))
			{
				return BadRequest("Invalid user name.");
			}
			User newUser = new User()
			{
				Name = userName
			};
			newUser.Id = _users.Max(c => c.Id) + 1;

			_users.Add(newUser);
			return CreatedAtAction(nameof(GetUsers), new { id = newUser.Id }, newUser);
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteUser(int id)
		{
			var user = _users.FirstOrDefault(u => u.Id == id);
			if (user == null)
			{
				return NotFound($"User with ID {id} not found.");
			}

			_users.Remove(user);
			return NoContent();
		}
	}
}
