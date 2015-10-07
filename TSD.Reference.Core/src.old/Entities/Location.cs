
namespace TSD.Reference.Core.Entities
{
	public class Location
	{
		public int Id { get; set; }
		/// <summary>
		/// Customer that location belongs to
		/// </summary>
		public int CustomerId { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string PostalCode { get; set; }
		public string Country { get; set; }
		public float? Latitude { get; set; }
		public float? Longitude { get; set; }
	}
}