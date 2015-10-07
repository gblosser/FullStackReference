
namespace TSD.Reference.Core.Entities
{
	public class Driver
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string FullName => $"{FirstName} {LastName}";
		public string Address { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string PostalCode { get; set; }
		public string Country { get; set; }
		public string LicenseNumber { get; set; }
		public string LicenseState { get; set; }
		public int CustomerId { get; set; }
	}
}