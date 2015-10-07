using TSD.Reference.Core.Data;
using TSD.Reference.Core.Entities;

namespace TSD.Reference.Core.Services
{
	public class LocationService
	{
		private readonly ILocationRepository _repository;
		public LocationService(ILocationRepository theLocationRepository)
		{
			_repository = theLocationRepository;
		}

		public Location GetLocation(int theLocationId)
		{
			return _repository.GetLocation(theLocationId);
		}
		public int AddLocation(Location theLocation )
		{
			return _repository.AddLocation(theLocation);
		}

		public void UpdateLocation(Location theLocation)
		{
			_repository.UpdateLocation(theLocation);
		}

		public void DeleteLocation(Location theLocation)
		{
			_repository.DeleteLocation(theLocation);
		}
	}
}
