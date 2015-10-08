using System;
using System.Linq;
using SQLite;
using TSD.Reference.Core.Entities;

namespace TSD.Reference.Data.SQLite.DTO
{
	internal class RentalAgreementDTO
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		[Indexed]
		public int CustomerId { get; set; }
		[Indexed]
		public int LocationId { get; set; }
		[Indexed]
		public int RenterId { get; set; }
		/// <summary>
		/// ;;-delimited string list of driver id integers
		/// </summary>
		public string AdditionalDrivers { get; set; }
		public DateTime OutDate { get; set; }
		public DateTime InDate { get; set; }
		public int AutomobileId { get; set; }
		/// <summary>
		/// ;;-delimited string list of additions
		/// </summary>
		public string Additions { get; set; }
		/// <summary>
		/// Status indicates if the agreement is a quote, reservation, open agreement, closed agreement or cancelled agreement
		/// </summary>
		public string Status { get; set; }
		public int EmployeeId { get; set; }
	}

	internal static class RentalAgreementExtensions
	{
		internal static RentalAgreementDTO ToDTO(this RentalAgreement d)
		{
			var aAdditionalDrivers = string.Join(";;", d.AdditionalDrivers);
			var aAdditions = string.Join(";;", d.Additions);

			return new RentalAgreementDTO
			{
				AdditionalDrivers = aAdditionalDrivers,
				Additions = aAdditions,
				AutomobileId = d.Automobile,
				CustomerId = d.Customer,
				Id = d.Id,
				InDate = d.InDate,
				LocationId = d.Location,
				OutDate = d.OutDate,
				RenterId = d.Renter,
				Status = d.Status,
				EmployeeId = d.EmployeeId
			};
		}

		internal static RentalAgreement ToEntity(this RentalAgreementDTO d)
		{
			var aDriverStrings = d.AdditionalDrivers.Split(';', ';');
			var aAdditionalDrivers = aDriverStrings.Select(aItem => Convert.ToInt32(aItem)).ToList();

			var aSplitAdditions = d.Additions.Split(';', ';');

			return new RentalAgreement
			{
				AdditionalDrivers = aAdditionalDrivers,
				Additions = !aSplitAdditions.Any() ? Enumerable.Empty<string>().ToList() : aSplitAdditions.ToList(),
				Automobile = d.AutomobileId,
				Customer = d.CustomerId,
				Id = d.Id,
				InDate = d.InDate,
				Location = d.LocationId,
				OutDate = d.OutDate,
				Renter = d.RenterId,
				Status = d.Status,
				EmployeeId = d.EmployeeId
			};
		}
	}
}
