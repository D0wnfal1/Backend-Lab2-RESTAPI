using Backend_Lab2_RESTAPI.Models.Dto;
using Backend_Lab2_RESTAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Backend_Lab2_RESTAPI.Data;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

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
			JwtSecurityTokenHandler tokenHandler = new();
			byte[] key = Encoding.ASCII.GetBytes(secretKey);

			SecurityTokenDescriptor tokenDescriptor = new()
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim("fullName", userFromDb.Name),
					new Claim("id", userFromDb.Id.ToString()),
					new Claim(ClaimTypes.Email, userFromDb.UserName.ToString()),
				}),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

			LoginResponseDto loginResponse = new()
			{
				Email = userFromDb.Email,
				Token = tokenHandler.WriteToken(token)
			};

			if (loginResponse.Email == null || string.IsNullOrEmpty(loginResponse.Token))
			{
				return BadRequest();
			}
			return Ok(loginResponse);
		}

		[HttpGet("test")]
		[Authorize]
		public async Task<ActionResult<string>> AuthTest()
		{
			return "You are authenticated";
		}
	}
}