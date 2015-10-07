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
			try
			{
				Connection.Open();

				var aPreparedCommand =
					new NpgsqlCommand(
						"SELECT id, name, allowsadditionaldrivers, allowsadditions, hasmaxrentaldays, maxrentaldays from customer where id = :value1", Connection);
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
			finally
			{
				if (Connection.State == ConnectionState.Open)
					Connection.Close();
			}
		}

		public Customer GetCustomerByName(string theCustomerName)
		{
			try
			{
				Connection.Open();

				var aPreparedCommand =
					new NpgsqlCommand(
						"SELECT id, name, allowsadditionaldrivers, allowsadditions, hasmaxrentaldays, maxrentaldays from customer where name = :value1", Connection);
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
			finally
			{
				if (Connection.State == ConnectionState.Open)
					Connection.Close();
			}
		}

		public int AddCustomer(Customer theCustomer)
		{
			try
			{
				Connection.Open();

				var aCommand = new NpgsqlCommand(
					"Insert into customer (name, allowsadditionaldrivers, allowsadditions, hasmaxrentaldays, maxrentaldays) VALUES (:value1, :value2, :value3, :value4, :value5) RETURNING id", Connection);
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
			finally
			{
				if (Connection.State == ConnectionState.Open)
					Connection.Close();
			}
		}

		public void UpdateCustomer(Customer theCustomer)
		{
			try
			{
				Connection.Open();

				var aCommand = new NpgsqlCommand(
					"UPDATE customer SET name=:value1, allowsadditionaldrivers=:value2, allowsadditions=:value3, hasmaxrentaldays=:value4, maxrentaldays=:value5 where id=:value6;", Connection);
				aCommand.Parameters.AddWithValue("value1", theCustomer.Name);
				aCommand.Parameters.AddWithValue("value2", theCustomer.AllowsAdditionalDrivers);
				aCommand.Parameters.AddWithValue("value3", theCustomer.AllowsAdditions);
				aCommand.Parameters.AddWithValue("value4", theCustomer.HasMaxRentalDays);
				aCommand.Parameters.AddWithValue("value5", theCustomer.MaxRentalDays);
				aCommand.Parameters.AddWithValue("value6", theCustomer.Id);

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

		public void DeleteCustomer(Customer theCustomer)
		{
			try
			{
				Connection.Open();

				var aCommand = new NpgsqlCommand("DELETE from customer where id=:value1", Connection);
				aCommand.Parameters.AddWithValue("value1", theCustomer.Id);

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

		public async Task<Customer> GetCustomerAsync(int theCustomerId)
		{
			try
			{
				await Connection.OpenAsync().ConfigureAwait(false);

				var aPreparedCommand =
					new NpgsqlCommand(
						"SELECT id, name, allowsadditionaldrivers, allowsadditions, hasmaxrentaldays, maxrentaldays from customer where id = :value1", Connection);
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
			finally
			{
				if (Connection.State == ConnectionState.Open)
					Connection.Close();
			}
		}

		public async Task<Customer> GetCustomerByNameAsync(string theCustomerName)
		{
			try
			{
				await Connection.OpenAsync().ConfigureAwait(false);

				var aPreparedCommand =
					new NpgsqlCommand(
						"SELECT id, name, allowsadditionaldrivers, allowsadditions, hasmaxrentaldays, maxrentaldays from customer where name = :value1", Connection);
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
			finally
			{
				if (Connection.State == ConnectionState.Open)
					Connection.Close();
			}
		}

		public async Task<int> AddCustomerAsync(Customer theCustomer)
		{
			try
			{
				await Connection.OpenAsync().ConfigureAwait(false);

				var aCommand = new NpgsqlCommand(
					"Insert into customer (name, allowsadditionaldrivers, allowsadditions, hasmaxrentaldays, maxrentaldays) VALUES (:value1, :value2, :value3, :value4, :value5) RETURNING id", Connection);
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
			finally
			{
				if (Connection.State == ConnectionState.Open)
					Connection.Close();
			}
		}

		public async Task UpdateCustomerAsync(Customer theCustomer)
		{
			try
			{
				await Connection.OpenAsync().ConfigureAwait(false);

				var aCommand = new NpgsqlCommand(
					"UPDATE customer SET name=:value1, allowsadditionaldrivers=:value2, allowsadditions=:value3, hasmaxrentaldays=:value4, maxrentaldays=:value5 where id=:value6;", Connection);
				aCommand.Parameters.AddWithValue("value1", theCustomer.Name);
				aCommand.Parameters.AddWithValue("value2", theCustomer.AllowsAdditionalDrivers);
				aCommand.Parameters.AddWithValue("value3", theCustomer.AllowsAdditions);
				aCommand.Parameters.AddWithValue("value4", theCustomer.HasMaxRentalDays);
				aCommand.Parameters.AddWithValue("value5", theCustomer.MaxRentalDays);
				aCommand.Parameters.AddWithValue("value6", theCustomer.Id);

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

		public async Task DeleteCustomerAsync(Customer theCustomer)
		{
			try
			{
				await Connection.OpenAsync().ConfigureAwait(false);

				var aCommand = new NpgsqlCommand("DELETE from customer where id=:value1", Connection);
				aCommand.Parameters.AddWithValue("value1", theCustomer.Id);

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
