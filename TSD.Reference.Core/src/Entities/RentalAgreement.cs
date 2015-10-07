using System;
using System.Collections.Generic;

namespace TSD.Reference.Core.Entities
{
	public class RentalAgreement
	{
		public int Id { get; set; }
		public int Customer { get; set; }
		public int Location { get; set; }
		public int Renter { get; set; }
		public List<int> AdditionalDrivers { get; set; } 
		public DateTime OutDate { get; set; }
		public DateTime InDate { get; set; }
		public int Automobile { get; set; }
		public List<string> Additions { get; set; }
		/// <summary>
		/// Status indicates if the agreement is a quote, reservation, open agreement, closed agreement or cancelled agreement
		/// </summary>
		public string Status { get; set; }
	}
}