
namespace TSD.Reference.Core.Entities
{
	public class Customer
	{
		public int Id { get; set; }
		public string Name { get; set; }
		/// <summary>
		/// If true, this customer has the ability to add additional 
		/// drivers to rental agreements
		/// </summary>
		public bool AllowsAdditionalDrivers { get; set; }
		/// <summary>
		/// If true, this customer has the ability to 
		/// add extras to rental agreements (gps, baby seat, etc...)
		/// </summary>
		public bool AllowsAdditions { get; set; }
		/// <summary>
		/// If true, this customer has a limitation on the length of a rental
		/// </summary>
		public bool HasMaxRentalDays { get; set; }
		/// <summary>
		/// The maximum number of days that a rental agreement can be open if HasMaxRentalDays
		/// is true
		/// </summary>
		public int MaxRentalDays { get; set; }
	}
}