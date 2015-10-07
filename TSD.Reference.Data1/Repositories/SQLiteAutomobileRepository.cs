using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using TSD.Reference.Core.Data;
using TSD.Reference.Core.Entities;
using TSD.Reference.Data.DTO;

namespace TSD.Reference.Data.Repositories
{
	public class SQLiteAutomobileRepository : IAutomobileRepository
	{
		private SQLiteConnection dbConnection => new SQLiteConnection(SQLiteConstants.ConnectionString);
		private SQLiteAsyncConnection dbConnectionAsync => new SQLiteAsyncConnection(SQLiteConstants.ConnectionString);


		public SQLiteAutomobileRepository()
		{ 
			var db = dbConnection;
			db.CreateTable<AutomobileDTO>(CreateFlags.AutoIncPK);
		}

		#region Sync methods

		public Automobile GetAutomobile(int theAutomobileId)
		{
			var aDto = dbConnection.Table<AutomobileDTO>().Where(aItem => aItem.Id == theAutomobileId).FirstOrDefault();
			return aDto?.ToEntity();
		}

		public List<Automobile> GetAutomobiles(IEnumerable<int> theAutomobileIds)
		{
			var aResult = dbConnection.Table<AutomobileDTO>().Where(aItem => theAutomobileIds.Contains(aItem.Id));

			// if there are no results return an empty list
			if (aResult == null || !aResult.Any())
				return Enumerable.Empty<Automobile>().ToList();

			return aResult.Select(aItem => aItem.ToEntity()).ToList();
		}

		public int AddAutomobile(Automobile theAutomobile)
		{
			return dbConnection.Insert(theAutomobile.ToDTO());
		}

		public void UpdateAutomobile(Automobile theAutomobile)
		{
			var aAuto = dbConnection.Update(theAutomobile.ToDTO());
		}

		public void DeleteAutomobile(Automobile theAutomobile)
		{
			var aAuto = dbConnection.Delete<AutomobileDTO>(theAutomobile.Id);
		}

		#endregion Sync methods

		#region Async methods

		public async Task<Automobile> GetAutomobileAsync(int theAutomobileId)
		{
			var aDto =
				await dbConnectionAsync.Table<AutomobileDTO>().Where(aItem => aItem.Id == theAutomobileId).FirstOrDefaultAsync();
			return aDto?.ToEntity();
		}

		public async Task<List<Automobile>> GetAutomobilesAsync(IEnumerable<int> theAutomobileIds)
		{
			var aResult =
				await dbConnectionAsync.Table<AutomobileDTO>().Where(aItem => theAutomobileIds.Contains(aItem.Id)).ToListAsync();
			return aResult.Select(aItem => aItem.ToEntity()).ToList();
		}

		public async Task<int> AddAutomobileAsync(Automobile theAutomobile)
		{
			return await dbConnectionAsync.InsertAsync(theAutomobile.ToDTO());
		}

		public async Task UpdateAutomobileAsync(Automobile theAutomobile)
		{
			var aAuto = await dbConnectionAsync.UpdateAsync(theAutomobile.ToDTO());
		}

		public async Task DeleteAutomobileAsync(Automobile theAutomobile)
		{
			var aAuto = await dbConnectionAsync.DeleteAsync(theAutomobile.ToDTO());
		}

		#endregion Async methods
	}
}
