using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Backend_Lab2_RESTAPI.Models
{
	public class Record
	{
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
		private DateTime _createtime;

		[Required]
		public DateTime CreateTime
		{
			get => _createtime;
			set
			{
				if (value > DateTime.UtcNow)
					throw new ArgumentException("Record date cannot be in the future.");
				_createtime = DateTime.SpecifyKind(value, DateTimeKind.Utc);
			}
		}
		public decimal Amount { get; set; }
    }
}
