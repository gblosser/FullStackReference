using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using NpgsqlTypes;
using TSD.Reference.Core.Data;
using TSD.Reference.Core.Entities;

namespace TSD.Reference.Data.PostgreSQL.Repositories
{
	public class AutomobileRepository : AbstractRepository, IAutomobileRepository
	{
		public Automobile GetAutomobile(int theAutomobileId)
		{
			try
			{
				Connection.Open();

				var aPreparedCommand =
					new NpgsqlCommand(
						"SELECT id, vin, vehiclenumber, name, class, style, color, manufacturer, model, code, locationid from automobile where id = :value1", Connection);
				var aParam = new NpgsqlParameter("value1", NpgsqlDbType.Integer) { Value = theAutomobileId };
				aPreparedCommand.Parameters.Add(aParam);

				var aReader = aPreparedCommand.ExecuteReader();

				if (!aReader.HasRows)
					return null;

				var aReturn = new Automobile();
				while (aReader.Read())
				{
					aReturn = ReadAutomobile(aReader);
				}
				return aReturn;
			}
			catch (NpgsqlException)
			{
				return null;
			}
			catch (InvalidOperationException)
			{
				return null;
			}
			catch (SqlException)
			{
				return null;
			}
			catch (ConfigurationErrorsException)
			{
				return null;
			}
			finally
			{
				if (Connection.State == ConnectionState.Open)
					Connection.Close();
			}
		}

		public IEnumerable<Automobile> GetAutomobiles(IEnumerable<int> theAutomobileIds)
		{
			try
			{
				Connection.Open();

				var aPreparedCommand =
					new NpgsqlCommand(
						"SELECT id, vin, vehiclenumber, name, class, style, color, manufacturer, model, code, locationid from automobile where id = :value1", Connection);
				aPreparedCommand.Parameters.AddWithValue("value1", string.Join(",", theAutomobileIds));

				var aReader = aPreparedCommand.ExecuteReader();

				if (!aReader.HasRows)
					return Enumerable.Empty<Automobile>().ToList();

				var aReturnList = new List<Automobile>();
				while (aReader.Read())
				{
					Automobile aReturn = new Automobile();
					aReturn.Id = Convert.ToInt32(aReader["id"]);
					aReturn.Class = Convert.ToString(aReader["class"]);
					aReturn.Code = Convert.ToString(aReader["code"]);
					aReturn.Color = Convert.ToString(aReader["color"]);
					aReturn.LocationId = Convert.ToInt32(aReader["locationid"]);
					aReturn.Manufacturer = Convert.ToString(aReader["manufacturer"]);
					aReturn.Model = Convert.ToString(aReader["model"]);
					aReturn.Name = Convert.ToString(aReader["name"]);
					aReturn.Style = Convert.ToString(aReader["style"]);
					aReturn.VIN = Convert.ToString(aReader["vin"]);
					aReturn.VehicleNumber = Convert.ToString(aReader["vehiclenumber"]);
					aReturnList.Add(aReturn);
				}
				return aReturnList;
			}
			catch (NpgsqlException)
			{
				return Enumerable.Empty<Automobile>().ToList();
			}
			catch (InvalidOperationException)
			{
				return Enumerable.Empty<Automobile>().ToList();
			}
			catch (SqlException)
			{
				return Enumerable.Empty<Automobile>().ToList();
			}
			catch (ConfigurationErrorsException)
			{
				return Enumerable.Empty<Automobile>().ToList();
			}
			finally
			{
				if (Connection.State == ConnectionState.Open)
					Connection.Close();
			}
		}

		public async Task<IEnumerable<Automobile>> GetAutomobilesForLocationAsync(int theLocationId)
		{
			try
			{
				await Connection.OpenAsync();

				var aPreparedCommand =
					new NpgsqlCommand(
						"SELECT id, vin, vehiclenumber, name, class, style, color, manufacturer, model, code, locationid from automobile where locationid = :value1", Connection);
				aPreparedCommand.Parameters.AddWithValue("value1", string.Join(",", theLocationId));

				var aReader = aPreparedCommand.ExecuteReader();

				if (!aReader.HasRows)
					return Enumerable.Empty<Automobile>().ToList();

				var aReturnList = new List<Automobile>();
				while (aReader.Read())
				{
					Automobile aReturn = new Automobile();
					aReturn.Id = Convert.ToInt32(aReader["id"]);
					aReturn.Class = Convert.ToString(aReader["class"]);
					aReturn.Code = Convert.ToString(aReader["code"]);
					aReturn.Color = Convert.ToString(aReader["color"]);
					aReturn.LocationId = Convert.ToInt32(aReader["locationid"]);
					aReturn.Manufacturer = Convert.ToString(aReader["manufacturer"]);
					aReturn.Model = Convert.ToString(aReader["model"]);
					aReturn.Name = Convert.ToString(aReader["name"]);
					aReturn.Style = Convert.ToString(aReader["style"]);
					aReturn.VIN = Convert.ToString(aReader["vin"]);
					aReturn.VehicleNumber = Convert.ToString(aReader["vehiclenumber"]);
					aReturnList.Add(aReturn);
				}
				return aReturnList;
			}
			catch (NpgsqlException)
			{
				return Enumerable.Empty<Automobile>().ToList();
			}
			catch (InvalidOperationException)
			{
				return Enumerable.Empty<Automobile>().ToList();
			}
			catch (SqlException)
			{
				return Enumerable.Empty<Automobile>().ToList();
			}
			catch (ConfigurationErrorsException)
			{
				return Enumerable.Empty<Automobile>().ToList();
			}
			finally
			{
				if (Connection.State == ConnectionState.Open)
					Connection.Close();
			}
		}

		public async Task<IEnumerable<Automobile>> GetAutomobilesForLocationsAsync(IEnumerable<int> theLocationIds)
		{
			try
			{
				await Connection.OpenAsync();

				var aPreparedCommand =
					new NpgsqlCommand(
						"SELECT id, vin, vehiclenumber, name, class, style, color, manufacturer, model, code, locationid from automobile where locationid in :value1", Connection);
				aPreparedCommand.Parameters.AddWithValue("value1", string.Join(",", string.Join(",", theLocationIds)));

				var aReader = aPreparedCommand.ExecuteReader();

				if (!aReader.HasRows)
					return Enumerable.Empty<Automobile>().ToList();

				var aReturnList = new List<Automobile>();
				while (aReader.Read())
				{
					Automobile aReturn = new Automobile();
					aReturn.Id = Convert.ToInt32(aReader["id"]);
					aReturn.Class = Convert.ToString(aReader["class"]);
					aReturn.Code = Convert.ToString(aReader["code"]);
					aReturn.Color = Convert.ToString(aReader["color"]);
					aReturn.LocationId = Convert.ToInt32(aReader["locationid"]);
					aReturn.Manufacturer = Convert.ToString(aReader["manufacturer"]);
					aReturn.Model = Convert.ToString(aReader["model"]);
					aReturn.Name = Convert.ToString(aReader["name"]);
					aReturn.Style = Convert.ToString(aReader["style"]);
					aReturn.VIN = Convert.ToString(aReader["vin"]);
					aReturn.VehicleNumber = Convert.ToString(aReader["vehiclenumber"]);
					aReturnList.Add(aReturn);
				}
				return aReturnList;
			}
			catch (NpgsqlException)
			{
				return Enumerable.Empty<Automobile>().ToList();
			}
			catch (InvalidOperationException)
			{
				return Enumerable.Empty<Automobile>().ToList();
			}
			catch (SqlException)
			{
				return Enumerable.Empty<Automobile>().ToList();
			}
			catch (ConfigurationErrorsException)
			{
				return Enumerable.Empty<Automobile>().ToList();
			}
			finally
			{
				if (Connection.State == ConnectionState.Open)
					Connection.Close();
			}
		}

		public int AddAutomobile(Automobile theAutomobile)
		{
			try
			{
				Connection.Open();

				var aCommand = new NpgsqlCommand(
					"Insert into automobile (vin, vehiclenumber, name, class, style, color, manufacturer, model, code, locationid) VALUES (:value1, :value2, :value3, :value4, :value5, :value6, :value7, :value8, :value9, :value10) RETURNING id", Connection);
				aCommand.Parameters.AddWithValue("value1", theAutomobile.VIN);
				aCommand.Parameters.AddWithValue("value2", theAutomobile.VehicleNumber);
				aCommand.Parameters.AddWithValue("value3", theAutomobile.Name);
				aCommand.Parameters.AddWithValue("value4", theAutomobile.Class);
				aCommand.Parameters.AddWithValue("value5", theAutomobile.Style);
				aCommand.Parameters.AddWithValue("value6", theAutomobile.Color);
				aCommand.Parameters.AddWithValue("value7", theAutomobile.Manufacturer);
				aCommand.Parameters.AddWithValue("value8", theAutomobile.Model);
				aCommand.Parameters.AddWithValue("value9", theAutomobile.Code);
				aCommand.Parameters.AddWithValue("value10", theAutomobile.LocationId);

				// returns the id from the SELECT, RETURNING sql statement above
				return Convert.ToInt32(aCommand.ExecuteScalar());
			}
			catch (NpgsqlException)
			{
				return 0;
			}
			catch (InvalidOperationException)
			{
				return 0;
			}
			catch (SqlException)
			{
				return 0;
			}
			catch (ConfigurationErrorsException)
			{
				return 0;
			}
			finally
			{
				if (Connection.State == ConnectionState.Open)
					Connection.Close();
			}
		}

		public void UpdateAutomobile(Automobile theAutomobile)
		{
			try
			{
				Connection.Open();

				var aCommand = new NpgsqlCommand(
					"UPDATE automobile SET vin=:value1, vehiclenumber=:value2, name=:value3, class=:value4, style=:value5, color=:value6, manufacturer=:value7, model=:value8, code=:value9, locationid=:value10 where id=:value11;", Connection);
				aCommand.Parameters.AddWithValue("value1", theAutomobile.VIN);
				aCommand.Parameters.AddWithValue("value2", theAutomobile.VehicleNumber);
				aCommand.Parameters.AddWithValue("value3", theAutomobile.Name);
				aCommand.Parameters.AddWithValue("value4", theAutomobile.Class);
				aCommand.Parameters.AddWithValue("value5", theAutomobile.Style);
				aCommand.Parameters.AddWithValue("value6", theAutomobile.Color);
				aCommand.Parameters.AddWithValue("value7", theAutomobile.Manufacturer);
				aCommand.Parameters.AddWithValue("value8", theAutomobile.Model);
				aCommand.Parameters.AddWithValue("value9", theAutomobile.Code);
				aCommand.Parameters.AddWithValue("value10", theAutomobile.LocationId);
				aCommand.Parameters.AddWithValue("value11", theAutomobile.Id);

				aCommand.ExecuteNonQuery();
			}
			// no catch here, this is a reference project
			// TODO: add catch and actions here
			finally
			{
				if (Connection.State == ConnectionState.Open)
					Connection.Close();
			}
		}

		public void DeleteAutomobile(Automobile theAutomobile)
		{
			try
			{
				Connection.Open();

				var aCommand = new NpgsqlCommand("DELETE from automobile where id=:value1", Connection);
				aCommand.Parameters.AddWithValue("value1", theAutomobile.Id);

				aCommand.ExecuteNonQuery();
			}
			// no catch here, this is a reference project
			// TODO: add catch and actions here
			finally
			{
				if (Connection.State == ConnectionState.Open)
					Connection.Close();
			}
		}

		public async Task<Automobile> GetAutomobileAsync(int theAutomobileId)
		{
			try
			{
				await Connection.OpenAsync().ConfigureAwait(false);

				var aPreparedCommand =
					new NpgsqlCommand(
						"SELECT id, vin, vehiclenumber, name, class, style, color, manufacturer, model, code, locationid from automobile where id = :value1", Connection);
				var aParam = new NpgsqlParameter("value1", NpgsqlDbType.Integer) { Value = theAutomobileId };
				aPreparedCommand.Parameters.Add(aParam);

				var aReader = await aPreparedCommand.ExecuteReaderAsync().ConfigureAwait(false);

				if (!aReader.HasRows)
					return null;

				var aReturn = new Automobile();
				while (await aReader.ReadAsync().ConfigureAwait(false))
				{
					aReturn = ReadAutomobile(aReader);
				}
				return aReturn;
			}
			catch (NpgsqlException)
			{
				return null;
			}
			catch (InvalidOperationException)
			{
				return null;
			}
			catch (SqlException)
			{
				return null;
			}
			catch (ConfigurationErrorsException)
			{
				return null;
			}
			finally
			{
				if (Connection.State == ConnectionState.Open)
					Connection.Close();
			}
		}

		public async Task<IEnumerable<Automobile>> GetAutomobilesAsync(IEnumerable<int> theAutomobileIds)
		{
			try
			{
				await Connection.OpenAsync().ConfigureAwait(false);

				var aPreparedCommand =
					new NpgsqlCommand(
						"SELECT id, vin, vehiclenumber, name, class, style, color, manufacturer, model, code, locationid from automobile where id = :value1", Connection);
				aPreparedCommand.Parameters.AddWithValue("value1", string.Join(",", theAutomobileIds));

				var aReader = await aPreparedCommand.ExecuteReaderAsync();

				if (!aReader.HasRows)
					return Enumerable.Empty<Automobile>().ToList();

				var aReturnList = new List<Automobile>();
				while (await aReader.ReadAsync())
				{
					aReturnList.Add(ReadAutomobile(aReader));
				}
				return aReturnList;
			}
			catch (NpgsqlException)
			{
				return Enumerable.Empty<Automobile>().ToList();
			}
			catch (InvalidOperationException)
			{
				return Enumerable.Empty<Automobile>().ToList();
			}
			catch (SqlException)
			{
				return Enumerable.Empty<Automobile>().ToList();
			}
			catch (ConfigurationErrorsException)
			{
				return Enumerable.Empty<Automobile>().ToList();
			}
			finally
			{
				if (Connection.State == ConnectionState.Open)
					Connection.Close();
			}
		}

		public async Task<int> AddAutomobileAsync(Automobile theAutomobile)
		{
			try
			{
				await Connection.OpenAsync().ConfigureAwait(false);

				var aCommand = new NpgsqlCommand(
					"Insert into automobile (vin, vehiclenumber, name, class, style, color, manufacturer, model, code, locationid) VALUES (:value1, :value2, :value3, :value4, :value5, :value6, :value7, :value8, :value9, :value10) RETURNING id", Connection);
				aCommand.Parameters.AddWithValue("value1", theAutomobile.VIN);
				aCommand.Parameters.AddWithValue("value2", theAutomobile.VehicleNumber);
				aCommand.Parameters.AddWithValue("value3", theAutomobile.Name);
				aCommand.Parameters.AddWithValue("value4", theAutomobile.Class);
				aCommand.Parameters.AddWithValue("value5", theAutomobile.Style);
				aCommand.Parameters.AddWithValue("value6", theAutomobile.Color);
				aCommand.Parameters.AddWithValue("value7", theAutomobile.Manufacturer);
				aCommand.Parameters.AddWithValue("value8", theAutomobile.Model);
				aCommand.Parameters.AddWithValue("value9", theAutomobile.Code);
				aCommand.Parameters.AddWithValue("value10", theAutomobile.LocationId);

				// returns the id from the SELECT, RETURNING sql statement above
				return Convert.ToInt32(aCommand.ExecuteScalarAsync().ConfigureAwait(false));
			}
			catch (NpgsqlException)
			{
				return 0;
			}
			catch (InvalidOperationException)
			{
				return 0;
			}
			catch (SqlException)
			{
				return 0;
			}
			catch (ConfigurationErrorsException)
			{
				return 0;
			}
			finally
			{
				if (Connection.State == ConnectionState.Open)
					Connection.Close();
			}
		}

		public async Task UpdateAutomobileAsync(Automobile theAutomobile)
		{
			try
			{
				await Connection.OpenAsync().ConfigureAwait(false);

				var aCommand = new NpgsqlCommand(
					"UPDATE automobile SET vin=:value1, vehiclenumber=:value2, name=:value3, class=:value4, style=:value5, color=:value6, manufacturer=:value7, model=:value8, code=:value9, locationid=:value10 where id=:value11;", Connection);
				aCommand.Parameters.AddWithValue("value1", theAutomobile.VIN);
				aCommand.Parameters.AddWithValue("value2", theAutomobile.VehicleNumber);
				aCommand.Parameters.AddWithValue("value3", theAutomobile.Name);
				aCommand.Parameters.AddWithValue("value4", theAutomobile.Class);
				aCommand.Parameters.AddWithValue("value5", theAutomobile.Style);
				aCommand.Parameters.AddWithValue("value6", theAutomobile.Color);
				aCommand.Parameters.AddWithValue("value7", theAutomobile.Manufacturer);
				aCommand.Parameters.AddWithValue("value8", theAutomobile.Model);
				aCommand.Parameters.AddWithValue("value9", theAutomobile.Code);
				aCommand.Parameters.AddWithValue("value10", theAutomobile.LocationId);
				aCommand.Parameters.AddWithValue("value11", theAutomobile.Id);

				await aCommand.ExecuteNonQueryAsync().ConfigureAwait(false);
			}
			// no catch here, this is a reference project
			// TODO: add catch and actions here
			finally
			{
				if (Connection.State == ConnectionState.Open)
					Connection.Close();
			}
		}

		public async Task DeleteAutomobileAsync(Automobile theAutomobile)
		{
			try
			{
				await Connection.OpenAsync().ConfigureAwait(false);

				var aCommand = new NpgsqlCommand("DELETE from automobile where id=:value1", Connection);
				aCommand.Parameters.AddWithValue("value1", theAutomobile.Id);

				await aCommand.ExecuteNonQueryAsync().ConfigureAwait(false);
			}
			// no catch here, this is a reference project
			// TODO: add catch and actions here
			finally
			{
				if (Connection.State == ConnectionState.Open)
					Connection.Close();
			}
		}


		private static Automobile ReadAutomobile(IDataRecord aReader)
		{
			var aReturn = new Automobile
			{
				Id = Convert.ToInt32(aReader["id"]),
				Class = Convert.ToString(aReader["class"]),
				Code = Convert.ToString(aReader["code"]),
				Color = Convert.ToString(aReader["color"]),
				LocationId = Convert.ToInt32(aReader["locationid"]),
				Manufacturer = Convert.ToString(aReader["manufacturer"]),
				Model = Convert.ToString(aReader["model"]),
				Name = Convert.ToString(aReader["name"]),
				Style = Convert.ToString(aReader["style"]),
				VIN = Convert.ToString(aReader["vin"]),
				VehicleNumber = Convert.ToString(aReader["vehiclenumber"])
			};

			return aReturn;
		}
	}
}
