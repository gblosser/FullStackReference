using SQLite;
using TSD.Reference.Core.Entities;

namespace TSD.Reference.Data.SQLite.DTO
{
	internal class DriverDTO
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }

		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string PostalCode { get; set; }
		public string Country { get; set; }
		public string LicenseNumber { get; set; }
		public string LicenseState { get; set; }
		public int CustomerId { get; set; }
	}

	internal static class DriverExtensions
	{
		/// <summary>
		/// Converts an Driver object to am Driver DTO object.
		/// DTO objects are stored in the database
		/// </summary>
		/// <param name="d">The Driver object</param>
		/// <returns>The DriverDTO object</returns>
		internal static Driver ToEntity(this DriverDTO d)
		{
			return new Driver
			{
				Address = d.Address,
				City = d.City,
				Country = d.Country,
				FirstName = d.FirstName,
				LastName = d.LastName,
				Id = d.Id,
				LicenseNumber = d.LicenseNumber,
				LicenseState = d.LicenseState,
				PostalCode = d.PostalCode,
				State = d.State,
				CustomerId = d.CustomerId
			};
		}

		/// <summary>
		/// Converts a Driver object to a Driver DTO object.
		/// DTO objects are stored in the database
		/// </summary>
		/// <param name="c">The Driver object</param>
		/// <returns>The Driver DTO object</returns>
		internal static DriverDTO ToDTO(this Driver d)
		{
			return new DriverDTO
			{
				Address = d.Address,
				City = d.City,
				Country = d.Country,
				FirstName = d.FirstName,
				LastName = d.LastName,
				Id = d.Id,
				LicenseNumber = d.LicenseNumber,
				LicenseState = d.LicenseState,
				PostalCode = d.PostalCode,
				State = d.State,
				CustomerId = d.CustomerId
			};
		}
	}
}
