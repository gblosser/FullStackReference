using System.Threading.Tasks;
using TSD.Reference.Core.Data;
using TSD.Reference.Core.Entities;
using TSD.Reference.Data.SQLite.DTO;

namespace TSD.Reference.Data.SQLite.Repositories
{
	public class RenterRepository : AbstractRepository, IRenterRepository
	{
		public RenterRepository()
		{
			var db = Connection;
			db.CreateTable<RenterDTO>();
		}

		public Renter GetRenter(int theRenterId)
		{
			var aRenter = Connection.Table<RenterDTO>().FirstOrDefault(aItem => aItem.Id == theRenterId);
			return aRenter?.ToEntity();
		}

		public int AddRenter(Renter theRenter)
		{
			return Connection.Insert(theRenter.ToDTO());
		}

		public void UpdateRenter(Renter theRenter)
		{
			Connection.Update(theRenter.ToDTO());
		}

		public void DeleteRenter(Renter theRenter)
		{
			Connection.Delete<RenterDTO>(theRenter.Id);
		}

		public async Task<Renter> GetRenterAsync(int theRenterId)
		{
			var aRenter = await ConnectionAsync.Table<RenterDTO>().Where(aItem => aItem.Id == theRenterId).FirstOrDefaultAsync();
			return aRenter?.ToEntity();
		}

		public async Task<int> AddRenterAsync(Renter theRenter)
		{
			return await ConnectionAsync.InsertAsync(theRenter.ToDTO());
		}

		public async Task UpdateRenterAsync(Renter theRenter)
		{
			await ConnectionAsync.UpdateAsync(theRenter.ToDTO());
		}

		public async Task DeleteRenterAsync(Renter theRenter)
		{
			await ConnectionAsync.DeleteAsync(theRenter.ToDTO());
		}
	}
}
