using Backend_Lab2_RESTAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Lab2_RESTAPI.Controllers;

[Route("record")]
[ApiController]
public class RecordController : ControllerBase
{
	private static List<Record> _records = new List<Record>
	{
		new Record { Id = 1, UserId = 1, CategoryId = 1, CreateTime = DateTime.Now, Amount = 100},
		new Record { Id = 2, UserId = 3, CategoryId = 1, CreateTime = DateTime.Now, Amount = 200},
		new Record { Id = 3, UserId = 2, CategoryId = 2, CreateTime = DateTime.Now, Amount = 300},
	};

	[HttpGet("{id}")]
	public IActionResult GetRecordById(int id)
	{
		var record = _records.FirstOrDefault(p => p.Id == id);
		if (record == null)
		{
			return NotFound();
		}
		return Ok(record);
	}

	[HttpDelete("{id}")]
	public IActionResult DeleteRecord(int id)
	{
		var record = _records.FirstOrDefault(p => p.Id == id);
		if (record == null)
		{
			return NotFound();
		}
		_records.Remove(record);
		return Ok(record);
	}

	[HttpPost]
	public IActionResult CreateRecord(int userId, int categoryId, decimal total)
	{
		Record record = new Record()
		{
			UserId = userId,
			CategoryId = categoryId,
			CreateTime = DateTime.Now,
			Amount = total
		};
		record.Id = _records.Max(p => p.Id) + 1;
		_records.Add(record);
		return CreatedAtAction(nameof(GetRecordById), new { id = record.Id }, record);
	}

	[HttpGet]
	public IActionResult GetRecord(int? userId, int? categoryId)
	{
		if (userId == null && categoryId == null)
		{
			return BadRequest($"User with ID {userId} or {categoryId} not found.");
		}

		var filteredRecords = _records.AsQueryable();

		if (userId != null)
		{
			filteredRecords = filteredRecords.Where(p => p.UserId == userId);
		}

		if (categoryId != null)
		{
			filteredRecords = filteredRecords.Where(p => p.CategoryId == categoryId);
		}

		if (!filteredRecords.Any())
		{
			return NotFound("No records found with the specified criteria.");
		}

		return Ok(filteredRecords);
	}
}