namespace Backend_Lab1_RESTAPI.Models
{
	public class Record
	{
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreateTime { get; set; }
        public decimal Amount { get; set; }
    }
}
