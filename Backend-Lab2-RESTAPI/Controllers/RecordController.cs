using Backend_Lab1_RESTAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Backend_Lab1_RESTAPI.Controllers
{
	[Route("record")]
	[ApiController]
	public class RecordController : ControllerBase
	{
		private static List<Record> _records = new List<Record>
		{
			new Record { Id = 1, UserId = 1, CategoryId = 1, CreateTime = DateTime.Now, Amount = 100.50m },
			new Record { Id = 2, UserId = 2, CategoryId = 2, CreateTime = DateTime.Now, Amount = 200.75m }
		};

		[HttpGet("{userId, categoryId}")]
		public IActionResult GetRecords([FromQuery] int? userId = null, [FromQuery] int? categoryId = null)
		{
			if (!userId.HasValue && !categoryId.HasValue)
			{
				return BadRequest("Parameters userId or categoryId must be specified.");
			}

			var filteredRecords = _records.AsQueryable();

			if (userId.HasValue)
			{
				filteredRecords = filteredRecords.Where(r => r.UserId == userId.Value);
			}

			if (categoryId.HasValue)
			{
				filteredRecords = filteredRecords.Where(r => r.CategoryId == categoryId.Value);
			}

			return Ok(filteredRecords.ToList());
		}

		[HttpGet("{id}")]
		public IActionResult GetRecordById(int id)
		{
			var record = _records.FirstOrDefault(r => r.Id == id);
			if (record == null)
			{
				return NotFound($"Record with ID {id} not found.");
			}
			return Ok(record);
		}

		[HttpPost]
		public IActionResult CreateRecord([FromBody] Record newRecord)
		{
			if (newRecord == null)
			{
				return BadRequest("Invalid record data.");
			}

			newRecord.Id = _records.Any() ? _records.Max(r => r.Id) + 1 : 1;
			_records.Add(newRecord);
			return CreatedAtAction(nameof(GetRecordById), new { id = newRecord.Id }, newRecord);
		}

		[HttpPut("{id}")]
		public IActionResult UpdateRecord(int id, [FromBody] Record updatedRecord)
		{
			var record = _records.FirstOrDefault(r => r.Id == id);
			if (record == null)
			{
				return NotFound($"Record with ID {id} not found.");
			}

			record.UserId = updatedRecord.UserId;
			record.CategoryId = updatedRecord.CategoryId;
			record.CreateTime = updatedRecord.CreateTime;
			record.Amount = updatedRecord.Amount;

			return NoContent();
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteRecord(int id)
		{
			var record = _records.FirstOrDefault(r => r.Id == id);
			if (record == null)
			{
				return NotFound($"Record with ID {id} not found.");
			}

			_records.Remove(record);
			return NoContent();
		}
	}
}
