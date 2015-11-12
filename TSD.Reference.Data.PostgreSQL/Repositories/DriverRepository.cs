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
	public class DriverRepository : AbstractRepository, IDriverRepository
	{
		public Driver GetDriver(int theDriverId)
		{
			using (var aConnection = GetConnection())
			{
				aConnection.Open();
				using (var aPreparedCommand = new NpgsqlCommand())
				{
					aPreparedCommand.Connection = aConnection;
					try
					{
						aPreparedCommand.CommandText =
							"SELECT id, firstname, lastname, address, city, state, postalcode, country, licensenumber, licensestate, customerid from driver where id = :value1";
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
				}
			}
		}

		public IEnumerable<Driver> GetDriverByLastName(string theDriverName)
		{
			using (var aConnection = GetConnection())
			{
				aConnection.Open();
				using (var aPreparedCommand = new NpgsqlCommand())
				{
					aPreparedCommand.Connection = aConnection;
					try
					{
						aPreparedCommand.CommandText =
							"SELECT id, firstname, lastname, address, city, state, postalcode, country, licensenumber, licensestate, customerid from driver where id = :value1";
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
				}
			}
		}

		public int AddDriver(Driver theDriver)
		{
			using (var aConnection = GetConnection())
			{
				aConnection.Open();
				using (var aCommand = new NpgsqlCommand())
				{
					aCommand.Connection = aConnection;
					try
					{
						aCommand.CommandText =
							"Insert into driver (firstname, lastname, address, city, state, postalcode, country, licensenumber, licensestate, customerid) VALUES (:value1, :value2, :value3, :value4, :value5, :value6, :value7, :value8, :value9, :value10) RETURNING id";
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
				}
			}
		}

		public void UpdateDriver(Driver theDriver)
		{
			using (var aConnection = GetConnection())
			{
				aConnection.Open();
				using (var aCommand = new NpgsqlCommand())
				{
					aCommand.Connection = aConnection;
					aCommand.CommandText =
						"UPDATE driver SET firstname = :value1, lastname = :value2, address = :value3, city = :value4, state = :value5, postalcode = :value6, country = :value7, licensenumber = :value8, licensestate = :value9, customerid = :value10 where id=:value11;";
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
			}
		}

		public void DeleteDriver(Driver theDriver)
		{
			using (var aConnection = GetConnection())
			{
				aConnection.Open();
				using (var aCommand = new NpgsqlCommand())
				{
					aCommand.Connection = aConnection;
					aCommand.CommandText = "DELETE from driver where id=:value1";
					aCommand.Parameters.AddWithValue("value1", theDriver.Id);

					aCommand.ExecuteNonQuery();
				}
			}
		}

		public async Task<Driver> GetDriverAsync(int theDriverId)
		{
			using (var aConnection = GetConnection())
			{
				await aConnection.OpenAsync();
				using (var aPreparedCommand = new NpgsqlCommand())
				{
					aPreparedCommand.Connection = aConnection;
					try
					{
						aPreparedCommand.CommandText =
							"SELECT id, firstname, lastname, address, city, state, postalcode, country, licensenumber, licensestate, customerid from driver where id = :value1";
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
				}
			}
		}

		public async Task<IEnumerable<Driver>> GetDriversByCustomerAsync(int theCustomerId)
		{
			using (var aConnection = GetConnection())
			{
				await aConnection.OpenAsync();
				using (var aPreparedCommand = new NpgsqlCommand())
				{
					aPreparedCommand.Connection = aConnection;
					try
					{
						aPreparedCommand.CommandText =
							"SELECT id, firstname, lastname, address, city, state, postalcode, country, licensenumber, licensestate, customerid from driver where customerid = :value1";
						var aParam = new NpgsqlParameter("value1", NpgsqlDbType.Integer) { Value = theCustomerId };
						aPreparedCommand.Parameters.Add(aParam);

						var aReader = await aPreparedCommand.ExecuteReaderAsync().ConfigureAwait(false);

						if (!aReader.HasRows)
							return Enumerable.Empty<Driver>();

						var aReturn = new List<Driver>();
						while (await aReader.ReadAsync().ConfigureAwait(false))
						{
							aReturn.Add(ReadDriver(aReader));
						}
						return aReturn;
					}
					catch (NpgsqlException)
					{
						return Enumerable.Empty<Driver>();
					}
					catch (InvalidOperationException)
					{
						return Enumerable.Empty<Driver>();
					}
					catch (SqlException)
					{
						return Enumerable.Empty<Driver>();
					}
					catch (ConfigurationErrorsException)
					{
						return Enumerable.Empty<Driver>();
					}
				}
			}
		}

		public async Task<IEnumerable<Driver>> GetDriverByLastNameAsync(string theDriverName)
		{
			using (var aConnection = GetConnection())
			{
				await aConnection.OpenAsync();
				using (var aPreparedCommand = new NpgsqlCommand())
				{
					aPreparedCommand.Connection = aConnection;
					try
					{
						aPreparedCommand.CommandText =
							"SELECT id, firstname, lastname, address, city, state, postalcode, country, licensenumber, licensestate, customerid from driver where id = :value1";
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
				}
			}
		}

		public async Task<int> AddDriverAsync(Driver theDriver)
		{
			using (var aConnection = GetConnection())
			{
				await aConnection.OpenAsync();
				using (var aCommand = new NpgsqlCommand())
				{
					aCommand.Connection = aConnection;
					try
					{
						aCommand.CommandText =
							"Insert into driver (firstname, lastname, address, city, state, postalcode, country, licensenumber, licensestate, customerid) VALUES (:value1, :value2, :value3, :value4, :value5, :value6, :value7, :value8, :value9, :value10) RETURNING id";
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
				}
			}
		}

		public async Task UpdateDriverAsync(Driver theDriver)
		{
			using (var aConnection = GetConnection())
			{
				await aConnection.OpenAsync();
				using (var aCommand = new NpgsqlCommand())
				{
					aCommand.Connection = aConnection;
					aCommand.CommandText =
						"UPDATE driver SET firstname = :value1, lastname = :value2, address = :value3, city = :value4, state = :value5, postalcode = :value6, country = :value7, licensenumber = :value8, licensestate = :value9, customerid = :value10 where id=:value11";
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
			}
		}

		public async Task DeleteDriverAsync(Driver theDriver)
		{
			using (var aConnection = GetConnection())
			{
				await aConnection.OpenAsync();
				using (var aCommand = new NpgsqlCommand())
				{
					aCommand.Connection = aConnection;
					aCommand.CommandText = "DELETE from driver where id=:value1";
					aCommand.Parameters.AddWithValue("value1", theDriver.Id);

					await aCommand.ExecuteNonQueryAsync().ConfigureAwait(false);
				}
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
