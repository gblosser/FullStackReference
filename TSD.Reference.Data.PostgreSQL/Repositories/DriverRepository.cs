using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Npgsql;
using NpgsqlTypes;
using TSD.Reference.Core.Data;
using TSD.Reference.Core.Entities;

namespace TSD.Reference.Data.PostgreSQL.Repositories
{
	public class DriverRepository : AbstractRepository, IDriverRepository
	{
		public Driver GetDriver(int theDriverId)
		{
			try
			{
				Connection.Open();

				var aPreparedCommand =
					new NpgsqlCommand(
						"SELECT id, firstname, lastname, address, city, state, postalcode, country, licensenumber, licensestate, customerid from driver where id = :value1", Connection);
				var aParam = new NpgsqlParameter("value1", NpgsqlDbType.Integer) { Value = theDriverId };
				aPreparedCommand.Parameters.Add(aParam);

				var aReader = aPreparedCommand.ExecuteReader();

				if (!aReader.HasRows)
					return null;

				var aReturn = new Driver();
				while (aReader.Read())
				{
					aReturn = ReadDriver(aReader);
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

		public List<Driver> GetDriverByLastName(string theDriverName)
		{
			try
			{
				Connection.Open();

				var aPreparedCommand =
					new NpgsqlCommand(
						"SELECT id, firstname, lastname, address, city, state, postalcode, country, licensenumber, licensestate, customerid from driver where id = :value1", Connection);
				var aParam = new NpgsqlParameter("value1", NpgsqlDbType.Text) { Value = theDriverName };
				aPreparedCommand.Parameters.Add(aParam);

				var aReader = aPreparedCommand.ExecuteReader();

				if (!aReader.HasRows)
					return null;

				var aReturn = new List<Driver>();
				while (aReader.Read())
				{
					aReturn.Add(ReadDriver(aReader));
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

		public int AddDriver(Driver theDriver)
		{
			try
			{
				Connection.Open();

				var aCommand = new NpgsqlCommand(
					"Insert into driver (firstname, lastname, address, city, state, postalcode, country, licensenumber, licensestate, customerid) VALUES (:value1, :value2, :value3, :value4, :value5, :value6, :value7, :value8, :value9, :value10) RETURNING id",
					Connection);
				aCommand.Parameters.AddWithValue("value1", theDriver.FirstName);
				aCommand.Parameters.AddWithValue("value2", theDriver.LastName);
				aCommand.Parameters.AddWithValue("value3", theDriver.Address);
				aCommand.Parameters.AddWithValue("value4", theDriver.City);
				aCommand.Parameters.AddWithValue("value5", theDriver.State);
				aCommand.Parameters.AddWithValue("value6", theDriver.PostalCode);
				aCommand.Parameters.AddWithValue("value7", theDriver.Country);
				aCommand.Parameters.AddWithValue("value8", theDriver.LicenseNumber);
				aCommand.Parameters.AddWithValue("value9", theDriver.LicenseState);
				aCommand.Parameters.AddWithValue("value10", theDriver.CustomerId);

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

		public void UpdateDriver(Driver theDriver)
		{
			try
			{
				Connection.Open();

				var aCommand = new NpgsqlCommand(
					"UPDATE driver SET firstname = :value1, lastname = :value2, address = :value3, city = :value4, state = :value5, postalcode = :value6, country = :value7, licensenumber = :value8, licensestate = :value9, customerid = :value10 where id=:value11;", Connection);
				aCommand.Parameters.AddWithValue("value1", theDriver.FirstName);
				aCommand.Parameters.AddWithValue("value2", theDriver.LastName);
				aCommand.Parameters.AddWithValue("value3", theDriver.Address);
				aCommand.Parameters.AddWithValue("value4", theDriver.City);
				aCommand.Parameters.AddWithValue("value5", theDriver.State);
				aCommand.Parameters.AddWithValue("value6", theDriver.PostalCode);
				aCommand.Parameters.AddWithValue("value7", theDriver.Country);
				aCommand.Parameters.AddWithValue("value8", theDriver.LicenseNumber);
				aCommand.Parameters.AddWithValue("value9", theDriver.LicenseState);
				aCommand.Parameters.AddWithValue("value10", theDriver.CustomerId);
				aCommand.Parameters.AddWithValue("value11", theDriver.Id);

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

		public void DeleteDriver(Driver theDriver)
		{
			try
			{
				Connection.Open();

				var aCommand = new NpgsqlCommand("DELETE from driver where id=:value1", Connection);
				aCommand.Parameters.AddWithValue("value1", theDriver.Id);

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

		public async Task<Driver> GetDriverAsync(int theDriverId)
		{
			try
			{
				await Connection.OpenAsync().ConfigureAwait(false);

				var aPreparedCommand =
					new NpgsqlCommand(
						"SELECT id, firstname, lastname, address, city, state, postalcode, country, licensenumber, licensestate, customerid from driver where id = :value1", Connection);
				var aParam = new NpgsqlParameter("value1", NpgsqlDbType.Integer) { Value = theDriverId };
				aPreparedCommand.Parameters.Add(aParam);

				var aReader = await aPreparedCommand.ExecuteReaderAsync().ConfigureAwait(false);

				if (!aReader.HasRows)
					return null;

				var aReturn = new Driver();
				while (await aReader.ReadAsync().ConfigureAwait(false))
				{
					aReturn = ReadDriver(aReader);
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

		public async Task<List<Driver>> GetDriverByLastNameAsync(string theDriverName)
		{
			try
			{
				await Connection.OpenAsync().ConfigureAwait(false);

				var aPreparedCommand =
					new NpgsqlCommand(
						"SELECT id, firstname, lastname, address, city, state, postalcode, country, licensenumber, licensestate, customerid from driver where id = :value1", Connection);
				var aParam = new NpgsqlParameter("value1", NpgsqlDbType.Text) { Value = theDriverName };
				aPreparedCommand.Parameters.Add(aParam);

				var aReader = await aPreparedCommand.ExecuteReaderAsync().ConfigureAwait(false);

				if (!aReader.HasRows)
					return null;

				var aReturn = new List<Driver>();
				while (await aReader.ReadAsync().ConfigureAwait(false))
				{
					aReturn.Add(ReadDriver(aReader));
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

		public async Task<int> AddDriverAsync(Driver theDriver)
		{
			try
			{
				await Connection.OpenAsync().ConfigureAwait(false);

				var aCommand = new NpgsqlCommand(
					"Insert into driver (firstname, lastname, address, city, state, postalcode, country, licensenumber, licensestate, customerid) VALUES (:value1, :value2, :value3, :value4, :value5, :value6, :value7, :value8, :value9, :value10) RETURNING id", Connection);
				aCommand.Parameters.AddWithValue("value1", theDriver.FirstName);
				aCommand.Parameters.AddWithValue("value2", theDriver.LastName);
				aCommand.Parameters.AddWithValue("value3", theDriver.Address);
				aCommand.Parameters.AddWithValue("value4", theDriver.City);
				aCommand.Parameters.AddWithValue("value5", theDriver.State);
				aCommand.Parameters.AddWithValue("value6", theDriver.PostalCode);
				aCommand.Parameters.AddWithValue("value7", theDriver.Country);
				aCommand.Parameters.AddWithValue("value8", theDriver.LicenseNumber);
				aCommand.Parameters.AddWithValue("value9", theDriver.LicenseState);
				aCommand.Parameters.AddWithValue("value10", theDriver.CustomerId);

				// returns the id from the SELECT, RETURNING sql statement above
				return Convert.ToInt32(await aCommand.ExecuteScalarAsync().ConfigureAwait(false));
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

		public async Task UpdateDriverAsync(Driver theDriver)
		{
			try
			{
				await Connection.OpenAsync().ConfigureAwait(false);

				var aCommand = new NpgsqlCommand(
					"UPDATE driver SET firstname = :value1, lastname = :value2, address = :value3, city = :value4, state = :value5, postalcode = :value6, country = :value7, licensenumber = :value8, licensestate = :value9, customerid = :value10 where id=:value11;", Connection);
				aCommand.Parameters.AddWithValue("value1", theDriver.FirstName);
				aCommand.Parameters.AddWithValue("value2", theDriver.LastName);
				aCommand.Parameters.AddWithValue("value3", theDriver.Address);
				aCommand.Parameters.AddWithValue("value4", theDriver.City);
				aCommand.Parameters.AddWithValue("value5", theDriver.State);
				aCommand.Parameters.AddWithValue("value6", theDriver.PostalCode);
				aCommand.Parameters.AddWithValue("value7", theDriver.Country);
				aCommand.Parameters.AddWithValue("value8", theDriver.LicenseNumber);
				aCommand.Parameters.AddWithValue("value9", theDriver.LicenseState);
				aCommand.Parameters.AddWithValue("value10", theDriver.CustomerId);
				aCommand.Parameters.AddWithValue("value11", theDriver.Id);

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

		public async Task DeleteDriverAsync(Driver theDriver)
		{
			try
			{
				await Connection.OpenAsync().ConfigureAwait(false);

				var aCommand = new NpgsqlCommand("DELETE from driver where id=:value1", Connection);
				aCommand.Parameters.AddWithValue("value1", theDriver.Id);

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

		private static Driver ReadDriver(IDataRecord aReader)
		{
			var aReturn = new Driver
			{
				Id = Convert.ToInt32(aReader["id"]),
				FirstName = Convert.ToString(aReader["firstname"]),
				LastName = Convert.ToString(aReader["lastname"]),
				Address = Convert.ToString(aReader["address"]),
				City = Convert.ToString(aReader["city"]),
				State = Convert.ToString(aReader["state"]),
				PostalCode = Convert.ToString(aReader["postalcode"]),
				Country = Convert.ToString(aReader["country"]),
				LicenseNumber = Convert.ToString(aReader["licensenumber"]),
				LicenseState = Convert.ToString(aReader["licensestate"]),
				CustomerId = Convert.ToInt32(aReader["customerid"])
			};

			return aReturn;
		}
	}
}
