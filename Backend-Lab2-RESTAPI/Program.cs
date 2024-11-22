using Backend_Lab2_RESTAPI.Data;
using Backend_Lab2_RESTAPI.Models;
using Backend_Lab2_RESTAPI.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddValidatorsFromAssemblyContaining<CategoryValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RecordValidator>(); 
builder.Services.AddValidatorsFromAssemblyContaining<UserValidator>(); 

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();



string? getEnv = Environment.GetEnvironmentVariable("RestApiDb");

builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseNpgsql(getEnv));

builder.Services.AddIdentityCore<ApplicationUser>()
	.AddEntityFrameworkStores<AppDbContext>()
	.AddDefaultTokenProviders();

builder.Services.AddDataProtection(); 

builder.Services.Configure<IdentityOptions>(options =>
{
	options.Password.RequireDigit = false;
	options.Password.RequiredLength = 1;
	options.Password.RequireLowercase = false;
	options.Password.RequireUppercase = false;
	options.Password.RequireNonAlphanumeric = false;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
	if (context.Database.CanConnect())
	{
		context.Database.EnsureDeleted();
	}
	context.Database.EnsureCreated();

	context.SaveChanges();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
