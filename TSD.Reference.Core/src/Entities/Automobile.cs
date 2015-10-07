using System;

namespace TSD.Reference.Core.Entities
{
	/// <summary>
	/// Vehicle represents a car that Customers can rent to Drivers
	/// </summary>
	public class Automobile
	{
		public int Id { get; set; }

		/// <summary>
		/// Vehicle Identification Number
		/// </summary>
		public string VIN { get; set; }

		/// <summary>
		/// Used as a way to "custom label" vehicles in a Customer system
		/// </summary>
		public string VehicleNumber { get; set; }
		public string Name { get; set; }
		public string Class { get; set; }

		public string Style { get; set; }
		public string Color { get; set; }
		public string Manufacturer { get; set; }
		public string Model { get; set; }
		public string Code { get; set; }

		public int LocationId { get; set; }
	}
}