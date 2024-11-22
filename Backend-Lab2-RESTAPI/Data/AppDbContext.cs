using Backend_Lab2_RESTAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Backend_Lab2_RESTAPI.Data
{
	public class AppDbContext : IdentityDbContext<ApplicationUser>
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		public DbSet<Category> Categories { get; set; }
		public DbSet<Record> Records { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Currency> Currencies { get; set; }
		public DbSet<ApplicationUser> ApplicationUsers { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<User>()
				.HasOne(u => u.DefaultCurrency)
				.WithMany()
				.HasForeignKey(u => u.DefaultCurrencyId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Record>()
				.HasOne<User>()
				.WithMany()
				.HasForeignKey(r => r.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Record>()
				.HasOne<Category>()
				.WithMany()
				.HasForeignKey(r => r.CategoryId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Record>()
				.HasOne(r => r.Currency)
				.WithMany()
				.HasForeignKey(r => r.CurrencyId)
				.OnDelete(DeleteBehavior.SetNull);

			modelBuilder.Entity<Currency>().HasData(
				new Currency { Id = 1, Name = "USD" },
				new Currency { Id = 2, Name = "EUR" },
				new Currency { Id = 3, Name = "UAH" }
			);

			modelBuilder.Entity<User>().HasData(
				new User { Id = 1, Name = "John Doe", DefaultCurrencyId = 1 },
				new User { Id = 2, Name = "Jane Smith", DefaultCurrencyId = 3 }
			);

			modelBuilder.Entity<Category>().HasData(
				new Category { Id = 1, CategoryName = "Food" },
				new Category { Id = 2, CategoryName = "Entertainment" }
			);

			modelBuilder.Entity<Record>().HasData(
				new Record
				{
					Id = 1,
					UserId = 1,
					CategoryId = 1,
					CreateTime = DateTime.UtcNow,
					Amount = 50.75m,
					CurrencyId = 2 
				},
				new Record
				{
					Id = 2,
					UserId = 2,
					CategoryId = 2,
					CreateTime = DateTime.UtcNow,
					Amount = 20.00m,
					CurrencyId = null 
				}
			);
		}
	}
}
