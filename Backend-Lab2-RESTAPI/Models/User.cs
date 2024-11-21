namespace Backend_Lab2_RESTAPI.Models
{
	public class User
	{
        public int Id { get; set; }
        public string Name { get; set; }
		public int DefaultCurrencyId { get; set; }
		public Currency DefaultCurrency { get; set; }
	}
}
