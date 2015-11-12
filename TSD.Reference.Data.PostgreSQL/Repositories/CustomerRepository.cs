using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Npgsql;
using NpgsqlTypes;
using TSD.Reference.Core.Data;
using TSD.Reference.Core.Entities;

namespace TSD.Reference.Data.PostgreSQL.Repositories
{
	public class CustomerRepository : AbstractRepository, ICustomerRepository
	{
		public Customer GetCustomer(int theCustomerId)
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
							"SELECT id, name, allowsadditionaldrivers, allowsadditions, hasmaxrentaldays, maxrentaldays from customer where id = :value1";
						var aParam = new NpgsqlParameter("value1", NpgsqlDbType.Integer) { Value = theCustomerId };
						aPreparedCommand.Parameters.Add(aParam);

						var aReader = aPreparedCommand.ExecuteReader();

						if (!aReader.HasRows)
							return null;

						var aReturn = new Customer();
						while (aReader.Read())
						{
							aReturn = ReadCustomer(aReader);
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

		public Customer GetCustomerByName(string theCustomerName)
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
							"SELECT id, name, allowsadditionaldrivers, allowsadditions, hasmaxrentaldays, maxrentaldays from customer where name = :value1";
						var aParam = new NpgsqlParameter("value1", NpgsqlDbType.Text) { Value = theCustomerName };
						aPreparedCommand.Parameters.Add(aParam);

						var aReader = aPreparedCommand.ExecuteReader();

						if (!aReader.HasRows)
							return null;

						var aReturn = new Customer();
						while (aReader.Read())
						{
							aReturn = ReadCustomer(aReader);
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

		public int AddCustomer(Customer theCustomer)
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
							"Insert into customer (name, allowsadditionaldrivers, allowsadditions, hasmaxrentaldays, maxrentaldays) VALUES (:value1, :value2, :value3, :value4, :value5) RETURNING id";
						aCommand.Parameters.AddWithValue("value1", theCustomer.Name);
						aCommand.Parameters.AddWithValue("value2", theCustomer.AllowsAdditionalDrivers);
						aCommand.Parameters.AddWithValue("value3", theCustomer.AllowsAdditions);
						aCommand.Parameters.AddWithValue("value4", theCustomer.HasMaxRentalDays);
						aCommand.Parameters.AddWithValue("value5", theCustomer.MaxRentalDays);

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

		public void UpdateCustomer(Customer theCustomer)
		{
			using (var aConnection = GetConnection())
			{
				aConnection.Open();
				using (var aCommand = new NpgsqlCommand())
				{
					aCommand.Connection = aConnection;
					aCommand.CommandText =
						"UPDATE customer SET name=:value1, allowsadditionaldrivers=:value2, allowsadditions=:value3, hasmaxrentaldays=:value4, maxrentaldays=:value5 where id=:value6;";
					aCommand.Parameters.AddWithValue("value1", theCustomer.Name);
					aCommand.Parameters.AddWithValue("value2", theCustomer.AllowsAdditionalDrivers);
					aCommand.Parameters.AddWithValue("value3", theCustomer.AllowsAdditions);
					aCommand.Parameters.AddWithValue("value4", theCustomer.HasMaxRentalDays);
					aCommand.Parameters.AddWithValue("value5", theCustomer.MaxRentalDays);
					aCommand.Parameters.AddWithValue("value6", theCustomer.Id);

					aCommand.ExecuteNonQuery();
				}
			}
		}

		public void DeleteCustomer(Customer theCustomer)
		{
			using (var aConnection = GetConnection())
			{
				aConnection.Open();
				using (var aCommand = new NpgsqlCommand())
				{
					aCommand.Connection = aConnection;
					aCommand.CommandText = "DELETE from customer where id=:value1";
					aCommand.Parameters.AddWithValue("value1", theCustomer.Id);

					aCommand.ExecuteNonQuery();
				}
			}
		}

		public async Task<Customer> GetCustomerAsync(int theCustomerId)
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
							"SELECT id, name, allowsadditionaldrivers, allowsadditions, hasmaxrentaldays, maxrentaldays from customer where id = :value1";
						var aParam = new NpgsqlParameter("value1", NpgsqlDbType.Integer) { Value = theCustomerId };
						aPreparedCommand.Parameters.Add(aParam);

						var aReader = await aPreparedCommand.ExecuteReaderAsync().ConfigureAwait(false);

						if (!aReader.HasRows)
							return null;

						var aReturn = new Customer();
						while (await aReader.ReadAsync().ConfigureAwait(false))
						{
							aReturn = ReadCustomer(aReader);
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

		public async Task<Customer> GetCustomerByNameAsync(string theCustomerName)
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
							"SELECT id, name, allowsadditionaldrivers, allowsadditions, hasmaxrentaldays, maxrentaldays from customer where name = :value1";
						var aParam = new NpgsqlParameter("value1", NpgsqlDbType.Text) { Value = theCustomerName };
						aPreparedCommand.Parameters.Add(aParam);

						var aReader = await aPreparedCommand.ExecuteReaderAsync().ConfigureAwait(false);

						if (!aReader.HasRows)
							return null;

						var aReturn = new Customer();
						while (await aReader.ReadAsync().ConfigureAwait(false))
						{
							aReturn = ReadCustomer(aReader);
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

		public async Task<int> AddCustomerAsync(Customer theCustomer)
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
							"Insert into customer (name, allowsadditionaldrivers, allowsadditions, hasmaxrentaldays, maxrentaldays) VALUES (:value1, :value2, :value3, :value4, :value5) RETURNING id";
						aCommand.Parameters.AddWithValue("value1", theCustomer.Name);
						aCommand.Parameters.AddWithValue("value2", theCustomer.AllowsAdditionalDrivers);
						aCommand.Parameters.AddWithValue("value3", theCustomer.AllowsAdditions);
						aCommand.Parameters.AddWithValue("value4", theCustomer.HasMaxRentalDays);
						aCommand.Parameters.AddWithValue("value5", theCustomer.MaxRentalDays);

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

		public async Task UpdateCustomerAsync(Customer theCustomer)
		{
			using (var aConnection = GetConnection())
			{
				await aConnection.OpenAsync();
				using (var aCommand = new NpgsqlCommand())
				{
					aCommand.Connection = aConnection;
					aCommand.CommandText =
						"UPDATE customer SET name=:value1, allowsadditionaldrivers=:value2, allowsadditions=:value3, hasmaxrentaldays=:value4, maxrentaldays=:value5 where id=:value6;";
					aCommand.Parameters.AddWithValue("value1", theCustomer.Name);
					aCommand.Parameters.AddWithValue("value2", theCustomer.AllowsAdditionalDrivers);
					aCommand.Parameters.AddWithValue("value3", theCustomer.AllowsAdditions);
					aCommand.Parameters.AddWithValue("value4", theCustomer.HasMaxRentalDays);
					aCommand.Parameters.AddWithValue("value5", theCustomer.MaxRentalDays);
					aCommand.Parameters.AddWithValue("value6", theCustomer.Id);

					await aCommand.ExecuteNonQueryAsync().ConfigureAwait(false);
				}
			}
		}

		public async Task DeleteCustomerAsync(Customer theCustomer)
		{
			using (var aConnection = GetConnection())
			{
				await aConnection.OpenAsync();
				using (var aCommand = new NpgsqlCommand())
				{
					aCommand.Connection = aConnection;
					aCommand.CommandText = "DELETE from customer where id=:value1";
					aCommand.Parameters.AddWithValue("value1", theCustomer.Id);

					await aCommand.ExecuteNonQueryAsync().ConfigureAwait(false);
				}
			}
		}


		private static Customer ReadCustomer(IDataRecord aReader)
		{
			var aReturn = new Customer
			{
				Id = Convert.ToInt32(aReader["id"]),
				Name = Convert.ToString(aReader["name"]),
				AllowsAdditionalDrivers = Convert.ToBoolean(aReader["allowsadditionaldrivers"]),
				AllowsAdditions = Convert.ToBoolean(aReader["allowsadditions"]),
				HasMaxRentalDays = Convert.ToBoolean(aReader["hasmaxrentaldays"]),
				MaxRentalDays = Convert.ToInt32(aReader["maxrentaldays"])
			};

			return aReturn;
		}
	}
}
