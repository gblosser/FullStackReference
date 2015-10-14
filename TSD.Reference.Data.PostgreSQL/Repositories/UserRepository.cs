using System;
using System.Collections.Generic;
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
	public class UserRepository : AbstractRepository, IUserRepository
	{
		public User GetUser(int theUserId)
		{
			try
			{
				Connection.Open();

				var aPreparedCommand =
					new NpgsqlCommand(
						"SELECT id, firstname, lastname, email, customerid, isemployee from renter where id = :value1", Connection);
				var aParam = new NpgsqlParameter("value1", NpgsqlDbType.Integer) { Value = theUserId };
				aPreparedCommand.Parameters.Add(aParam);

				var aReader = aPreparedCommand.ExecuteReader();

				if (!aReader.HasRows)
					return null;

				var aReturn = new User();
				while (aReader.Read())
				{
					aReturn = ReadUser(aReader);
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

		public User GetUserByEmail(string theEmail)
		{
			try
			{
				Connection.Open();

				var aPreparedCommand =
					new NpgsqlCommand(
						"SELECT id, firstname, lastname, email, customerid, isemployee from renter where email = :value1", Connection);
				var aParam = new NpgsqlParameter("value1", NpgsqlDbType.Text) { Value = theEmail };
				aPreparedCommand.Parameters.Add(aParam);

				var aReader = aPreparedCommand.ExecuteReader();

				if (!aReader.HasRows)
					return null;

				var aReturn = new User();
				while (aReader.Read())
				{
					aReturn = ReadUser(aReader);
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

		public int AddUser(User theUser)
		{
			try
			{
				Connection.Open();

				var aCommand = new NpgsqlCommand(
					"Insert into user (firstname, lastname,, email, customerid, isemployee) VALUES (:value1, :value2, :value3, :value4, :value5) RETURNING id", Connection);
				aCommand.Parameters.AddWithValue("value1", theUser.FirstName);
				aCommand.Parameters.AddWithValue("value2", theUser.LastName);
				aCommand.Parameters.AddWithValue("value3", theUser.Email);
				aCommand.Parameters.AddWithValue("value4", theUser.CustomerId);
				aCommand.Parameters.AddWithValue("value5", theUser.IsEmployee);

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

		public void UpdateUser(User theUser)
		{
			try
			{
				Connection.Open();

				var aCommand = new NpgsqlCommand(
					"UPDATE user SET firstname = :value1, lastname = :value2, email = :value3, customerid = :value4, isemployee = :value5 where id=:value6;", Connection);
				aCommand.Parameters.AddWithValue("value1", theUser.FirstName);
				aCommand.Parameters.AddWithValue("value2", theUser.LastName);
				aCommand.Parameters.AddWithValue("value3", theUser.Email);
				aCommand.Parameters.AddWithValue("value4", theUser.CustomerId);
				aCommand.Parameters.AddWithValue("value5", theUser.IsEmployee);
				aCommand.Parameters.AddWithValue("value6", theUser.Id);

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

		public void DeleteUser(User theUser)
		{
			try
			{
				Connection.Open();

				var aCommand = new NpgsqlCommand("DELETE from user where id=:value1", Connection);
				aCommand.Parameters.AddWithValue("value1", theUser.Id);

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

		public async Task<User> GetUserAsync(int theUserId)
		{
			try
			{
				await Connection.OpenAsync().ConfigureAwait(false);

				var aPreparedCommand =
					new NpgsqlCommand(
						"SELECT id, firstname, lastname, email, customerid, isemployee from renter where id = :value1", Connection);
				var aParam = new NpgsqlParameter("value1", NpgsqlDbType.Integer) { Value = theUserId };
				aPreparedCommand.Parameters.Add(aParam);

				var aReader = await aPreparedCommand.ExecuteReaderAsync().ConfigureAwait(false);

				if (!aReader.HasRows)
					return null;

				var aReturn = new User();
				while (await aReader.ReadAsync().ConfigureAwait(false))
				{
					aReturn = ReadUser(aReader);
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

		public async Task<IEnumerable<User>> GetUsersForCustomerAsync(int theCustomerId)
		{
			try
			{
				await Connection.OpenAsync().ConfigureAwait(false);

				var aPreparedCommand =
					new NpgsqlCommand(
						"SELECT id, firstname, lastname, email, customerid, isemployee from renter where customerid = :value1", Connection);
				var aParam = new NpgsqlParameter("value1", NpgsqlDbType.Integer) { Value = theCustomerId };
				aPreparedCommand.Parameters.Add(aParam);

				var aReader = await aPreparedCommand.ExecuteReaderAsync().ConfigureAwait(false);

				if (!aReader.HasRows)
					return null;

				var aReturn = new List< User>();
				while (await aReader.ReadAsync().ConfigureAwait(false))
				{
					aReturn.Add(ReadUser(aReader));
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

		public async Task<User> GetUserByEmailAsync(string theEmail)
		{
			try
			{
				await Connection.OpenAsync().ConfigureAwait(false);

				var aPreparedCommand =
					new NpgsqlCommand(
						"SELECT id, firstname, lastname, email, customerid, isemployee from renter where email = :value1", Connection);
				var aParam = new NpgsqlParameter("value1", NpgsqlDbType.Text) { Value = theEmail };
				aPreparedCommand.Parameters.Add(aParam);

				var aReader = await aPreparedCommand.ExecuteReaderAsync().ConfigureAwait(false);

				if (!aReader.HasRows)
					return null;

				var aReturn = new User();
				while (await aReader.ReadAsync().ConfigureAwait(false))
				{
					aReturn = ReadUser(aReader);
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

		public async Task<int> AddUserAsync(User theUser)
		{
			try
			{
				await Connection.OpenAsync().ConfigureAwait(false);

				var aCommand = new NpgsqlCommand(
					"Insert into user (firstname, lastname,, email, customerid, isemployee) VALUES (:value1, :value2, :value3, :value4, :value5) RETURNING id", Connection);
				aCommand.Parameters.AddWithValue("value1", theUser.FirstName);
				aCommand.Parameters.AddWithValue("value2", theUser.LastName);
				aCommand.Parameters.AddWithValue("value3", theUser.Email);
				aCommand.Parameters.AddWithValue("value4", theUser.CustomerId);
				aCommand.Parameters.AddWithValue("value5", theUser.IsEmployee);

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

		public async Task UpdateUserAsync(User theUser)
		{
			try
			{
				await Connection.OpenAsync().ConfigureAwait(false);

				var aCommand = new NpgsqlCommand(
					"UPDATE user SET firstname = :value1, lastname = :value2, email = :value3, customerid = :value4, isemployee = :value5 where id=:value6;", Connection);
				aCommand.Parameters.AddWithValue("value1", theUser.FirstName);
				aCommand.Parameters.AddWithValue("value2", theUser.LastName);
				aCommand.Parameters.AddWithValue("value3", theUser.Email);
				aCommand.Parameters.AddWithValue("value4", theUser.CustomerId);
				aCommand.Parameters.AddWithValue("value5", theUser.IsEmployee);
				aCommand.Parameters.AddWithValue("value6", theUser.Id);

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

		public async Task DeleteUserAsync(User theUser)
		{
			try
			{
				await Connection.OpenAsync().ConfigureAwait(false);

				var aCommand = new NpgsqlCommand("DELETE from user where id=:value1", Connection);
				aCommand.Parameters.AddWithValue("value1", theUser.Id);

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

		private static User ReadUser(IDataRecord aReader)
		{
			var aReturn = new User
			{
				Id = Convert.ToInt32(aReader["id"]),
				FirstName = Convert.ToString(aReader["firstname"]),
				LastName = Convert.ToString(aReader["lastname"]),
				Email = Convert.ToString(aReader["email"]),
				CustomerId = Convert.ToInt32(aReader["customerid"]),
				IsEmployee = Convert.ToBoolean(aReader["isemployee"])
			};

			return aReturn;
		}
	}
}
