using System;
using TSD.Reference.Core.Entities;

namespace TSD.Reference.Core.Data
{
	public interface IRenterRepository
	{
		Renter GetRenter(int theRenterId);
		int AddRenter(Renter theRenter);
		void UpdateRenter(Renter theRenter);
		void DeleteRenter(int theRenterId);
	}
}