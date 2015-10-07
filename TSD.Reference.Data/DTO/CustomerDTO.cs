using SQLite;
using TSD.Reference.Core.Entities;

namespace TSD.Reference.Data.SQLite.DTO
{
	internal class CustomerDTO
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public string Name { get; set; }
		public bool AllowsAdditionalDrivers { get; set; }
		public bool AllowsAdditions { get; set; }
		public bool HasMaxRentalDays { get; set; }
		public int MaxRentalDays { get; set; }
	}

	internal static class CustomerExtensions
	{
		/// <summary>
		/// Converts a Customer DTO object to a Customer object.
		/// DTO objects are stored in the database
		/// </summary>
		/// <param name="c">The CustomerDTO object</param>
		/// <returns>The Customer object</returns>
		internal static Customer ToEntity(this CustomerDTO c)
		{
			return new Customer
			{
				Id = c.Id,
				Name = c.Name,
				AllowsAdditionalDrivers = c.AllowsAdditionalDrivers,
				AllowsAdditions = c.AllowsAdditions,
				HasMaxRentalDays = c.HasMaxRentalDays,
				MaxRentalDays = c.MaxRentalDays
			};
		}

		/// <summary>
		/// Converts a Customer object to a Customer DTO object.
		/// DTO objects are stored in the database
		/// </summary>
		/// <param name="c">The Customer object</param>
		/// <returns>The Customer DTO object</returns>
		internal static CustomerDTO ToDTO(this Customer c)
		{
			return new CustomerDTO
			{
				Id = c.Id,
				Name = c.Name,
				AllowsAdditionalDrivers = c.AllowsAdditionalDrivers,
				AllowsAdditions = c.AllowsAdditions,
				HasMaxRentalDays = c.HasMaxRentalDays,
				MaxRentalDays = c.MaxRentalDays
			};
		}
	}
}
