namespace TSD.Reference.Core.Entities
{
	public class User
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public int CustomerId { get; set; }
		public bool IsEmployee { get; set; }
	}
}
