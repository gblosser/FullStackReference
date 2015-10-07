using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using TSD.Reference.Core.Entities;

namespace TSD.Reference.Data.DTO
{
	internal class RentalAgreementDTO
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		[Indexed]
		public int Customer { get; set; }
		[Indexed]
		public int Location { get; set; }
		[Indexed]
		public int Renter { get; set; }
		/// <summary>
		/// ;;-delimited string list of driver id integers
		/// </summary>
		public string AdditionalDrivers { get; set; }
		public DateTime OutDate { get; set; }
		public DateTime InDate { get; set; }
		public int Automobile { get; set; }
		/// <summary>
		/// ;;-delimited string list of additions
		/// </summary>
		public string Additions { get; set; }
		/// <summary>
		/// Status indicates if the agreement is a quote, reservation, open agreement, closed agreement or cancelled agreement
		/// </summary>
		public string Status { get; set; }
	}

	internal static class RentalAgreementExtensions
	{
		internal static RentalAgreementDTO ToDTO(RentalAgreement d)
		{
			var aAdditionalDrivers = string.Join(";;", d.AdditionalDrivers);
			var aAdditions = string.Join(";;", d.Additions);

			return new RentalAgreementDTO
			{
				AdditionalDrivers = aAdditionalDrivers,
				Additions = aAdditions,
				Automobile = d.Automobile,
				Customer = d.Customer,
				Id = d.Id,
				InDate = d.InDate,
				Location = d.Location,
				OutDate = d.OutDate,
				Renter = d.Renter,
				Status = d.Status
			};
		}

		internal static RentalAgreement ToEntity(RentalAgreementDTO d)
		{
			var aDriverStrings = d.AdditionalDrivers.Split(new[] {';', ';'});
			var aAdditionalDrivers = aDriverStrings.Select(aItem => Convert.ToInt32(aItem)).ToList();
			return new RentalAgreement
			{
				AdditionalDrivers = aAdditionalDrivers,
				Additions = d.Additions,
				Automobile = d.Automobile,
				Customer = d.Customer,
				Id = d.Id,
				InDate = d.InDate,
				Location = d.Location,
				OutDate = d.OutDate,
				Renter = d.Renter,
				Status = d.Status
			};
		}
	}
}
