using SQLite;
using TSD.Reference.Core.Entities;

namespace TSD.Reference.Data.DTO
{
	internal class LocationDTO
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		/// <summary>
		/// Customer that location belongs to
		/// </summary>
		[Indexed, NotNull]
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

	internal static class LocationExtensions
	{
		internal static Location ToEntity(this LocationDTO d)
		{
			return new Location
			{
				Address = d.Address,
				City = d.City,
				Country = d.Country,
				CustomerId = d.CustomerId,
				Id = d.Id,
				Latitude = d.Latitude,
				Longitude = d.Longitude,
				Name = d.Name,
				PostalCode = d.PostalCode,
				State = d.State
			};
		}

		internal static LocationDTO ToDTO(this Location d)
		{
			return new LocationDTO
			{
				Address = d.Address,
				City = d.City,
				Country = d.Country,
				CustomerId = d.CustomerId,
				Id = d.Id,
				Latitude = d.Latitude,
				Longitude = d.Longitude,
				Name = d.Name,
				PostalCode = d.PostalCode,
				State = d.State
			};
		}
	}
}
