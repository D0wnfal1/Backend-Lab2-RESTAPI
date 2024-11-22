using Backend_Lab2_RESTAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Backend_Lab2_RESTAPI.Data.DbInitializer
{
	public class DbInitialize : IDbInitializer
	{
		private readonly AppDbContext _db;

		public DbInitialize(AppDbContext db)
		{
			_db = db;
		}

		public void Initialize()
		{
			try
			{
				if (_db.Database.GetPendingMigrations().Any())
				{
					_db.Database.Migrate();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error during migration: " + ex.Message);
			}

			if (!_db.Currencies.Any())
			{
				_db.Currencies.AddRange(
					new Currency { Id = 1, Name = "USD" },
					new Currency { Id = 2, Name = "EUR" },
					new Currency { Id = 3, Name = "UAH" }
				);
				_db.SaveChanges();
			}

			if (!_db.Users.Any())
			{
				_db.Users.AddRange(
					new User { Id = 1, Name = "John Doe", DefaultCurrencyId = 1  },
					new User { Id = 2, Name = "Jane Smith", DefaultCurrencyId = 3 }
				);
				_db.SaveChanges();
			}

			if (!_db.Categories.Any())
			{
				_db.Categories.AddRange(
					new Category { Id = 1, CategoryName = "Food" },
					new Category { Id = 2, CategoryName = "Entertainment" }
				);
				_db.SaveChanges();
			}

			if (!_db.Records.Any())
			{
				_db.Records.AddRange(
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
						CurrencyId = 1 
					}
				);
				_db.SaveChanges();
			}
		}
	}
}
