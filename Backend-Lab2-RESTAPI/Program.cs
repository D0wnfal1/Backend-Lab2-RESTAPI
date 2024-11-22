using Backend_Lab2_RESTAPI.Data;
using Backend_Lab2_RESTAPI.Data.DbInitializer;
using Backend_Lab2_RESTAPI.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddValidatorsFromAssemblyContaining<CategoryValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RecordValidator>(); 
builder.Services.AddValidatorsFromAssemblyContaining<UserValidator>(); 
builder.Services.AddScoped<IDbInitializer, DbInitialize>();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddDbContext<AppDbContext>(options =>
{
	options.UseNpgsql(builder.Configuration.GetConnectionString("RestApiDb"));
});
builder.Services.AddScoped<IDbInitializer, DbInitialize>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
