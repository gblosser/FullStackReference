using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TSD.Reference.Core.Entities
{
	/// <summary>
	///  HotelRoom is a room that can be rented
	/// </summary>
	public class HotelRoom
	{
		public int Id { get; set; }

		/// <summary>
		/// Room Types can be Single, Double, Triple, Queen, King, Twin, Double-double, Studio, Mini-Suite, Suite, etc...
		/// </summary>
		[Required]
		public string RoomType { get; set; }

		[Required]
		public bool IsSmokingAllowed { get; set; }

		public int NumberOfOccupants { get; set; }

		[Required]
		public List<string> Beds { get; set; } 

		public List<string> Extras { get; set; } 

		[Required]
		public int LocationId { get; set; }
	}
}