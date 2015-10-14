using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSD.Reference.Core.Data;
using TSD.Reference.Core.Entities;
using TSD.Reference.Data.SQLite.DTO;

namespace TSD.Reference.Data.SQLite.Repositories
{
	public class RentalAgreementRepository : AbstractRepository, IRentalAgreementRepository
	{
		public RentalAgreementRepository()
		{
			var db = Connection;
			db.CreateTable<RentalAgreementDTO>();
		}

		public RentalAgreement GetRentalAgreement(int theRentalAgreementId)
		{
			var aRentalAgreement = Connection.Table<RentalAgreementDTO>().FirstOrDefault(aItem => aItem.Id == theRentalAgreementId);
			return aRentalAgreement?.ToEntity();
		}

		public int AddRentalAgreement(RentalAgreement theRentalAgreement)
		{
			return Connection.Insert(theRentalAgreement.ToDTO());
		}

		public void UpdateRentalAgreement(RentalAgreement theRentalAgreement)
		{
			Connection.Update(theRentalAgreement.ToDTO());
		}

		public void DeleteRentalAgreement(RentalAgreement theRentalAgreement)
		{
			Connection.Delete<RentalAgreementDTO>(theRentalAgreement.Id);
		}

		public async Task<RentalAgreement> GetRentalAgreementAsync(int theRentalAgreementId)
		{
			var aRentalAgreement = await ConnectionAsync.Table<RentalAgreementDTO>().Where(aItem => aItem.Id == theRentalAgreementId).FirstOrDefaultAsync();
			return aRentalAgreement?.ToEntity();
		}

		public async Task<int> AddRentalAgreementAsync(RentalAgreement theRentalAgreement)
		{
			return await ConnectionAsync.InsertAsync(theRentalAgreement);
		}

		public async Task UpdateRentalAgreementAsync(RentalAgreement theRentalAgreement)
		{
			await ConnectionAsync.UpdateAsync(theRentalAgreement);
		}

		public async Task DeleteRentalAgreementAsync(RentalAgreement theRentalAgreement)
		{
			await ConnectionAsync.DeleteAsync(theRentalAgreement);
		}

		public async Task<IEnumerable<RentalAgreement>> GetRentalAgreementsForCustomerAsync(int theCustomerId)
		{
			var aRentalAgreement = await ConnectionAsync.Table<RentalAgreementDTO>().Where(aItem => aItem.CustomerId == theCustomerId).ToListAsync();
			return aRentalAgreement.Select(aItem => aItem.ToEntity()).ToList();
		}  
	}
}
