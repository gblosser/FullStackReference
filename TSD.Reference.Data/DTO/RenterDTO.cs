using TSD.Reference.Core.Entities;

namespace TSD.Reference.Data.SQLite.DTO
{
	internal class RenterDTO
	{
		public string PaymentInfo { get; set; }
		public string InsuranceInfo { get; set; }

		#region Renter properties

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

		#endregion
	}

	internal static class RenterExtensions
	{
		internal static Renter ToEntity(this RenterDTO d)
		{
			return new Renter
			{
				Address = d.Address,
				City = d.City,
				Country = d.Country,
				CustomerId = d.CustomerId,
				FirstName = d.FirstName,
				Id = d.Id,
				InsuranceInfo = d.InsuranceInfo,
				LastName = d.LastName,
				LicenseNumber = d.LicenseNumber,
				LicenseState = d.LicenseState,
				PaymentInfo = d.PaymentInfo,
				PostalCode = d.PostalCode,
				State = d.State
			};
		}

		internal static RenterDTO ToDTO(this Renter d)
		{
			return new RenterDTO
			{
				Address = d.Address,
				City = d.City,
				Country = d.Country,
				CustomerId = d.CustomerId,
				FirstName = d.FirstName,
				Id = d.Id,
				InsuranceInfo = d.InsuranceInfo,
				LastName = d.LastName,
				LicenseNumber = d.LicenseNumber,
				LicenseState = d.LicenseState,
				PaymentInfo = d.PaymentInfo,
				PostalCode = d.PostalCode,
				State = d.State
			};
		}
	}
}
