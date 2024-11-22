using Backend_Lab2_RESTAPI.Models.Dto;
using Backend_Lab2_RESTAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Backend_Lab2_RESTAPI.Data;

namespace Backend_Lab2_RESTAPI.Controllers
{
	[Route("auth")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly AppDbContext _db;
		private readonly UserManager<ApplicationUser> _userManager;
		private string secretKey;
		public AuthController(AppDbContext db, IConfiguration configuration,
			UserManager<ApplicationUser> userManager)
		{
			_db = db;
			secretKey = configuration.GetValue<string>("ApiSettings:Token");
			_userManager = userManager;
		}
		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterRequestDto model)
		{
			ApplicationUser userFromDb = _db.ApplicationUsers
				.FirstOrDefault(u => u.UserName.ToLower() == model.UserName.ToLower());
			if (userFromDb != null)
			{
				return BadRequest();
			}
			ApplicationUser newUser = new()
			{
				UserName = model.UserName,
				Email = model.UserName,
				NormalizedEmail = model.UserName.ToUpper(),
				Name = model.Name
			};
			try
			{
				var result = await _userManager.CreateAsync(newUser, model.Password);
				if (result.Succeeded)
				{
					return Ok(result);
				}
			}
			catch (Exception)
			{
			}
			return BadRequest();
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
		{
			ApplicationUser userFromDb = _db.ApplicationUsers
					.FirstOrDefault(u => u.UserName.ToLower() == model.UserName.ToLower());
			bool isValid = await _userManager.CheckPasswordAsync(userFromDb, model.Password);
			if (isValid == false)
			{
				return BadRequest();
			}
			LoginResponseDto loginResponse = new()
			{
				Email = userFromDb.Email,
				Token = secretKey
			};
			if (loginResponse.Email == null || string.IsNullOrEmpty(loginResponse.Token))
			{
				return BadRequest();
			}
			return Ok(loginResponse);
		}
	}
}