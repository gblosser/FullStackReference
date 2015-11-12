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
	public class LocationRepository : AbstractRepository, ILocationRepository
	{
		public Location GetLocation(int theLocationId)
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
							"SELECT id, customerid, name, address, city, state, postalcode, country, latitude, longitude from location where id = :value1";
						var aParam = new NpgsqlParameter("value1", NpgsqlDbType.Integer) { Value = theLocationId };
						aPreparedCommand.Parameters.Add(aParam);

						var aReader = aPreparedCommand.ExecuteReader();

						if (!aReader.HasRows)
							return null;

						var aReturn = new Location();
						while (aReader.Read())
						{
							aReturn = ReadLocation(aReader);
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

		public int AddLocation(Location theLocation)
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
							"Insert into location (customerid, name, address, city, state, postalcode, country, latitude, longitude) VALUES (:value1, :value2, :value3, :value4, :value5, :value6, :value7, :value8, :value9) RETURNING id";
						aCommand.Parameters.AddWithValue("value1", theLocation.CustomerId);
						aCommand.Parameters.AddWithValue("value2", theLocation.Name);
						aCommand.Parameters.AddWithValue("value3", theLocation.Address);
						aCommand.Parameters.AddWithValue("value4", theLocation.City);
						aCommand.Parameters.AddWithValue("value5", theLocation.State);
						aCommand.Parameters.AddWithValue("value6", theLocation.PostalCode);
						aCommand.Parameters.AddWithValue("value7", theLocation.Country);
						aCommand.Parameters.AddWithValue("value8", theLocation.Latitude);
						aCommand.Parameters.AddWithValue("value9", theLocation.Longitude);

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

		public void UpdateLocation(Location theLocation)
		{
			using (var aConnection = GetConnection())
			{
				aConnection.Open();
				using (var aCommand = new NpgsqlCommand())
				{
					aCommand.Connection = aConnection;
					aCommand.CommandText =
						"UPDATE location SET customerid = :value1, name = :value2, address = :value3, city = :value4, state = :value5, postalcode = :value6, country = :value7, latitude = :value8, longitude = :value9 where id=:value10;";
					aCommand.Parameters.AddWithValue("value1", theLocation.CustomerId);
					aCommand.Parameters.AddWithValue("value2", theLocation.Name);
					aCommand.Parameters.AddWithValue("value3", theLocation.Address);
					aCommand.Parameters.AddWithValue("value4", theLocation.City);
					aCommand.Parameters.AddWithValue("value5", theLocation.State);
					aCommand.Parameters.AddWithValue("value6", theLocation.PostalCode);
					aCommand.Parameters.AddWithValue("value7", theLocation.Country);
					aCommand.Parameters.AddWithValue("value8", theLocation.Latitude);
					aCommand.Parameters.AddWithValue("value9", theLocation.Longitude);
					aCommand.Parameters.AddWithValue("value10", theLocation.Id);

					aCommand.ExecuteNonQuery();
				}
			}
		}

		public void DeleteLocation(Location theLocation)
		{
			using (var aConnection = GetConnection())
			{
				aConnection.Open();
				using (var aCommand = new NpgsqlCommand())
				{
					aCommand.Connection = aConnection;
					aCommand.CommandText = "DELETE from location where id=:value1";
					aCommand.Parameters.AddWithValue("value1", theLocation.Id);

					aCommand.ExecuteNonQuery();
				}
			}
		}

		public async Task<Location> GetLocationAsync(int theLocationId)
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
							"SELECT id, customerid, name, address, city, state, postalcode, country, latitude, longitude from location where id = :value1";
						var aParam = new NpgsqlParameter("value1", NpgsqlDbType.Integer) { Value = theLocationId };
						aPreparedCommand.Parameters.Add(aParam);

						var aReader = await aPreparedCommand.ExecuteReaderAsync().ConfigureAwait(false);

						if (!aReader.HasRows)
							return null;

						var aReturn = new Location();
						while (await aReader.ReadAsync().ConfigureAwait(false))
						{
							aReturn = ReadLocation(aReader);
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

		public async Task<int> AddLocationAsync(Location theLocation)
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
							"Insert into location (customerid, name, address, city, state, postalcode, country, latitude, longitude) VALUES (:value1, :value2, :value3, :value4, :value5, :value6, :value7, :value8, :value9) RETURNING id";
						aCommand.Parameters.AddWithValue("value1", theLocation.CustomerId);
						aCommand.Parameters.AddWithValue("value2", theLocation.Name);
						aCommand.Parameters.AddWithValue("value3", theLocation.Address);
						aCommand.Parameters.AddWithValue("value4", theLocation.City);
						aCommand.Parameters.AddWithValue("value5", theLocation.State);
						aCommand.Parameters.AddWithValue("value6", theLocation.PostalCode);
						aCommand.Parameters.AddWithValue("value7", theLocation.Country);
						aCommand.Parameters.AddWithValue("value8", theLocation.Latitude);
						aCommand.Parameters.AddWithValue("value9", theLocation.Longitude);

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

		public async Task UpdateLocationAsync(Location theLocation)
		{
			using (var aConnection = GetConnection())
			{
				await aConnection.OpenAsync();
				using (var aCommand = new NpgsqlCommand())
				{
					aCommand.Connection = aConnection;
					aCommand.CommandText =
						"UPDATE location SET customerid = :value1, name = :value2, address = :value3, city = :value4, state = :value5, postalcode = :value6, country = :value7, latitude = :value8, longitude = :value9 where id=:value10;";
					aCommand.Parameters.AddWithValue("value1", theLocation.CustomerId);
					aCommand.Parameters.AddWithValue("value2", theLocation.Name);
					aCommand.Parameters.AddWithValue("value3", theLocation.Address);
					aCommand.Parameters.AddWithValue("value4", theLocation.City);
					aCommand.Parameters.AddWithValue("value5", theLocation.State);
					aCommand.Parameters.AddWithValue("value6", theLocation.PostalCode);
					aCommand.Parameters.AddWithValue("value7", theLocation.Country);
					aCommand.Parameters.AddWithValue("value8", theLocation.Latitude);
					aCommand.Parameters.AddWithValue("value9", theLocation.Longitude);
					aCommand.Parameters.AddWithValue("value10", theLocation.Id);

					await aCommand.ExecuteNonQueryAsync().ConfigureAwait(false);
				}
			}
		}

		public async Task DeleteLocationAsync(Location theLocation)
		{
			using (var aConnection = GetConnection())
			{
				await aConnection.OpenAsync();
				using (var aCommand = new NpgsqlCommand())
				{
					aCommand.Connection = aConnection;
					aCommand.CommandText = "DELETE from location where id=:value1";
					aCommand.Parameters.AddWithValue("value1", theLocation.Id);

					await aCommand.ExecuteNonQueryAsync().ConfigureAwait(false);
				}
			}
				
		}

		public async Task<IEnumerable<Location>> GetLocationsForCustomerAsync(int theCustomerId)
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
							"SELECT id, customerid, name, address, city, state, postalcode, country, latitude, longitude  from location where customerid=:value1";
						aPreparedCommand.Parameters.AddWithValue("value1", theCustomerId);

						var aReader = await aPreparedCommand.ExecuteReaderAsync();

						if (!aReader.HasRows)
							return Enumerable.Empty<Location>();

						var aReturn = new List<Location>();
						while (await aReader.ReadAsync().ConfigureAwait(false))
						{
							aReturn.Add(ReadLocation(aReader));
						}
						return aReturn;
					}
					catch (NpgsqlException)
					{
						return Enumerable.Empty<Location>();
					}
					catch (InvalidOperationException ex)
					{
						return Enumerable.Empty<Location>();
					}
					catch (SqlException)
					{
						return Enumerable.Empty<Location>();
					}
					catch (ConfigurationErrorsException)
					{
						return Enumerable.Empty<Location>();
					}
				}
			}
		}

		private static Location ReadLocation(IDataRecord aReader)
		{
			var aReturn = new Location
			{
				Id = Convert.ToInt32(aReader["id"]),
				CustomerId = Convert.ToInt32(aReader["customerid"]),
				Name = Convert.ToString(aReader["name"]),
				Address = Convert.ToString(aReader["address"]),
				City = Convert.ToString(aReader["city"]),
				State = Convert.ToString(aReader["state"]),
				PostalCode = Convert.ToString(aReader["postalcode"]),
				Country = Convert.ToString(aReader["country"]),
				Latitude = Convert.ToSingle(aReader["latitude"]),
				Longitude = Convert.ToSingle(aReader["longitude"])
			};

			return aReturn;
		}
	}
}
