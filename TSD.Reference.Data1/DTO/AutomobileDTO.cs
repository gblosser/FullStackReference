using SQLite;
using TSD.Reference.Core.Entities;

namespace TSD.Reference.Data.DTO
{
	internal class AutomobileDTO
	{
		[PrimaryKey, AutoIncrement]
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

		[Indexed, NotNull]
		public string LocationId { get; set; }
	}

	internal static class AutomobileExtensions
	{
		/// <summary>
		/// Converts an Automobile object to am Automobile DTO object.
		/// DTO objects are stored in the database
		/// </summary>
		/// <param name="d">The Automobile object</param>
		/// <returns>The AutomobileDTO object</returns>
		internal static AutomobileDTO ToDTO(this Automobile d)
		{
			return new AutomobileDTO
			{
				Id = d.Id,
				Class = d.Class,
				Code = d.Code,
				Color = d.Color,
				Manufacturer = d.Manufacturer,
				Model = d.Model,
				Name = d.Name,
				Style = d.Style,
				VehicleNumber = d.VehicleNumber,
				VIN = d.VIN,
				LocationId = d.LocationId
			};
		}

		/// <summary>
		/// Converts an Automobile DTO object to an Automobile object
		/// </summary>
		/// <param name="d">The AutomobileDTO object</param>
		/// <returns>The Automobile object</returns>
		internal static Automobile ToEntity(this AutomobileDTO d)
		{
			return new Automobile
			{
				Id = d.Id,
				Class = d.Class,
				Code = d.Code,
				Color = d.Color,
				Manufacturer = d.Manufacturer,
				Model = d.Model,
				Name = d.Name,
				Style = d.Style,
				VehicleNumber = d.VehicleNumber,
				VIN = d.VIN,
				LocationId = d.LocationId
			};
		}
	}
}
