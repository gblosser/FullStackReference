using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TSD.Reference.Core.Entities;

namespace TSD.Reference.Core.Data
{
	public interface IAutomobileRepository
	{
		Automobile GetAutomobile(int theAutomobileId);
		List<Automobile> GetAutomobiles(IEnumerable<int> theAutomobileIds);
		int AddAutomobile(Automobile theAutomobile);
		void UpdateAutomobile(Automobile theAutomobile);
		void DeleteAutomobile(Automobile theAutomobile);

		Task<Automobile> GetAutomobileAsync(int theAutomobileId);
		Task<List<Automobile>> GetAutomobilesAsync(IEnumerable<int> theAutomobileIds);
		Task<int> AddAutomobileAsync(Automobile theAutomobile);
		Task UpdateAutomobileAsync(Automobile theAutomobile);
		Task DeleteAutomobileAsync(Automobile theAutomobile);
	}
}
