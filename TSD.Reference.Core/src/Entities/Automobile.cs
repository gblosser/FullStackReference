using System.ComponentModel.DataAnnotations;

namespace TSD.Reference.Core.Entities
{
	/// <summary>
	/// Automobile represents a car that Customers can rent to Drivers
	/// </summary>
	public class Automobile
	{
		public int Id { get; set; }

		/// <summary>
		/// Vehicle Identification Number
		/// </summary>
		[Required]
		public string VIN { get; set; }

		/// <summary>
		/// Used as a way to "custom label" vehicles in a Customer system
		/// </summary>
		public string VehicleNumber { get; set; }
		public string Name { get; set; }
		public string Class { get; set; }

		public string Style { get; set; }
		public string Color { get; set; }
		[Required]
		public string Manufacturer { get; set; }
		[Required]
		public string Model { get; set; }
		public string Code { get; set; }

		[Required]
		public int LocationId { get; set; }
	}
}