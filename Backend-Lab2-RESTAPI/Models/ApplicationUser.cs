using Microsoft.AspNetCore.Identity;

namespace Backend_Lab2_RESTAPI.Models
{
	public class ApplicationUser : IdentityUser
	{
		public string Name { get; set; }
	}
}
