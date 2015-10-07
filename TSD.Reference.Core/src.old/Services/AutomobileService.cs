using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TSD.Reference.Core.Data;
using TSD.Reference.Core.Entities;

namespace TSD.Reference.Core.Services
{
	public class AutomobileService
	{
		private readonly IAutomobileRepository _repository;
		public AutomobileService(IAutomobileRepository theRepository)
		{
			_repository = theRepository;
		}

		public List<Automobile> GetAutomobiles(IEnumerable<int> theAutomobileIds)
		{
			return _repository.GetAutomobiles(theAutomobileIds);
		}

		public async Task<List<Automobile>> GetAutomobilesAsync(IEnumerable<int> theAutomobileIds)
		{
			return await _repository.GetAutomobilesAsync(theAutomobileIds);
		}

		public int AddAutomobile(Automobile theAutomobile)
		{
			return _repository.AddAutomobile(theAutomobile);
		}

		public async Task<int> AddAutomobileAsync(Automobile theAutomobile)
		{
			return await _repository.AddAutomobileAsync(theAutomobile);
		}
	}
}
