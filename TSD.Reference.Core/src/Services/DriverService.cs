using System.Collections.Generic;
using TSD.Reference.Core.Data;
using TSD.Reference.Core.Entities;

namespace TSD.Reference.Core.Services
{
	public class DriverService
	{
		private readonly IDriverRepository _repository;

		public DriverService(IDriverRepository theRepository)
		{
			_repository = theRepository;
		}

		public List<Driver> GetDriverByLastName(string theLastName)
		{
			return _repository.GetDriverByLastName(theLastName);
		}

		public Driver GetDriver(int theDriverId)
		{
			return _repository.GetDriver(theDriverId);
		}

		public int AddDriver(Driver theDriver)
		{
			return _repository.AddDriver(theDriver);
		}
	}
}
