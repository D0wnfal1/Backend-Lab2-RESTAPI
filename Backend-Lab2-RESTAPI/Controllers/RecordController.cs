using Backend_Lab2_RESTAPI.Data;
using Backend_Lab2_RESTAPI.Models;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Backend_Lab2_RESTAPI.Validation;
using Microsoft.AspNetCore.Authorization;

namespace Backend_Lab2_RESTAPI.Controllers
{
	[Route("record")]
	[ApiController]
	[Authorize]
	public class RecordController : ControllerBase
	{
		private readonly AppDbContext _dbContext;
		private readonly IValidator<Record> _recordValidator;

		public RecordController(AppDbContext dbContext, IValidator<Record> recordValidator)
		{
			_dbContext = dbContext;
			_recordValidator = recordValidator;
		}

		[HttpGet("{id}")]
		public IActionResult GetRecordById(int id)
		{
			var record = _dbContext.Records.FirstOrDefault(p => p.Id == id);
			if (record == null)
			{
				return NotFound($"Record with ID {id} not found.");
			}
			return Ok(record);
		}

		[HttpPost]
		public IActionResult CreateRecord([FromBody] Record record)
		{
			var validationResult = _recordValidator.Validate(record);
			if (!validationResult.IsValid)
			{
				return BadRequest(validationResult.Errors);
			}

			record.Id = _dbContext.Records.Any() ? _dbContext.Records.Max(p => p.Id) + 1 : 1;
			record.CreateTime = DateTime.UtcNow;

			_dbContext.Records.Add(record);
			_dbContext.SaveChanges();
			return CreatedAtAction(nameof(GetRecordById), new { id = record.Id }, record);
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteRecord(int id)
		{
			var record = _dbContext.Records.FirstOrDefault(p => p.Id == id);
			if (record == null)
			{
				return NotFound($"Record with ID {id} not found.");
			}

			_dbContext.Records.Remove(record);
			_dbContext.SaveChanges();
			return NoContent();
		}

		[HttpGet]
		public IActionResult GetRecords([FromQuery] int? userId, [FromQuery] int? categoryId)
		{
			var filteredRecords = _dbContext.Records.AsQueryable();

			if (userId.HasValue)
			{
				filteredRecords = filteredRecords.Where(p => p.UserId == userId.Value);
			}

			if (categoryId.HasValue)
			{
				filteredRecords = filteredRecords.Where(p => p.CategoryId == categoryId.Value);
			}

			if (!filteredRecords.Any())
			{
				return NotFound("No records found with the specified criteria.");
			}

			return Ok(filteredRecords);
		}
	}
}
