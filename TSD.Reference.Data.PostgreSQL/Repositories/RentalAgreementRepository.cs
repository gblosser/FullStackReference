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
	public class RentalAgreementRepository : AbstractRepository, IRentalAgreementRepository
	{
		public RentalAgreement GetRentalAgreement(int theRentalAgreementId)
		{
			try
			{
				Connection.Open();

				var aPreparedCommand =
					new NpgsqlCommand(
						"SELECT id, customerid, locationid, renterid, additionaldrivers, outdate, indate, automobileid, additions, status, employeeid from rentalagreement where id = :value1", Connection);
				var aParam = new NpgsqlParameter("value1", NpgsqlDbType.Integer) { Value = theRentalAgreementId };
				aPreparedCommand.Parameters.Add(aParam);

				var aReader = aPreparedCommand.ExecuteReader();

				if (!aReader.HasRows)
					return null;

				var aReturn = new RentalAgreement();
				while (aReader.Read())
				{
					aReturn = ReadRentalAgreement(aReader);
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

		public int AddRentalAgreement(RentalAgreement theRentalAgreement)
		{
			try
			{
				Connection.Open();

				var aCommand = new NpgsqlCommand(
					"Insert into rentalagreement (customerid, locationid, renterid, additionaldrivers, outdate, indate, automobileid, additions, status, employeeid) VALUES (:value1, :value2, :value3, :value4, :value5, :value6, :value7, :value8, :value9, :value10) RETURNING id", Connection);
				aCommand.Parameters.AddWithValue("value1", theRentalAgreement.Customer);
				aCommand.Parameters.AddWithValue("value2", theRentalAgreement.Location);
				aCommand.Parameters.AddWithValue("value3", theRentalAgreement.Renter);
				aCommand.Parameters.AddWithValue("value4",
					theRentalAgreement.AdditionalDrivers.Any() ? string.Join(";;", theRentalAgreement.AdditionalDrivers) : string.Empty);
				aCommand.Parameters.AddWithValue("value5", theRentalAgreement.OutDate);
				aCommand.Parameters.AddWithValue("value6", theRentalAgreement.InDate);
				aCommand.Parameters.AddWithValue("value7", theRentalAgreement.Automobile);
				aCommand.Parameters.AddWithValue("value8",
					theRentalAgreement.Additions.Any() ? string.Join(";;", theRentalAgreement.Additions) : string.Empty);
				aCommand.Parameters.AddWithValue("value9", theRentalAgreement.Status);
				aCommand.Parameters.AddWithValue("value10", theRentalAgreement.EmployeeId);

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

		public void UpdateRentalAgreement(RentalAgreement theRentalAgreement)
		{
			try
			{
				Connection.Open();

				var aCommand = new NpgsqlCommand(
					"UPDATE rentalagreement SET customerid = :value1, locationid = :value2, renterid = :value3, additionaldrivers = :value4, outdate = :value5, indate = :value6, automobileid = :value7, additions = :value8, status = :value9, employeeid = :value10 where id=:value11;", Connection);
				aCommand.Parameters.AddWithValue("value1", theRentalAgreement.Customer);
				aCommand.Parameters.AddWithValue("value2", theRentalAgreement.Location);
				aCommand.Parameters.AddWithValue("value3", theRentalAgreement.Renter);
				aCommand.Parameters.AddWithValue("value4",
					theRentalAgreement.AdditionalDrivers.Any() ? string.Join(";;", theRentalAgreement.AdditionalDrivers) : string.Empty);
				aCommand.Parameters.AddWithValue("value5", theRentalAgreement.OutDate);
				aCommand.Parameters.AddWithValue("value6", theRentalAgreement.InDate);
				aCommand.Parameters.AddWithValue("value7", theRentalAgreement.Automobile);
				aCommand.Parameters.AddWithValue("value8",
					theRentalAgreement.Additions.Any() ? string.Join(";;", theRentalAgreement.Additions) : string.Empty);
				aCommand.Parameters.AddWithValue("value9", theRentalAgreement.Status);
				aCommand.Parameters.AddWithValue("value10", theRentalAgreement.EmployeeId);
				aCommand.Parameters.AddWithValue("value11", theRentalAgreement.Id);

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

		public void DeleteRentalAgreement(RentalAgreement theRentalAgreement)
		{
			try
			{
				Connection.Open();

				var aCommand = new NpgsqlCommand("DELETE from rentalagreement where id=:value1", Connection);
				aCommand.Parameters.AddWithValue("value1", theRentalAgreement.Id);

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

		public async Task<RentalAgreement> GetRentalAgreementAsync(int theRentalAgreementId)
		{
			try
			{
				await Connection.OpenAsync().ConfigureAwait(false);

				var aPreparedCommand =
					new NpgsqlCommand(
						"SELECT id, customerid, locationid, renterid, additionaldrivers, outdate, indate, automobileid, additions, status, employeeid from rentalagreement where id = :value1", Connection);
				var aParam = new NpgsqlParameter("value1", NpgsqlDbType.Integer) { Value = theRentalAgreementId };
				aPreparedCommand.Parameters.Add(aParam);

				var aReader = await aPreparedCommand.ExecuteReaderAsync().ConfigureAwait(false);

				if (!aReader.HasRows)
					return null;

				var aReturn = new RentalAgreement();
				while (await aReader.ReadAsync().ConfigureAwait(false))
				{
					aReturn = ReadRentalAgreement(aReader);
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

		public async Task<int> AddRentalAgreementAsync(RentalAgreement theRentalAgreement)
		{
			try
			{
				await Connection.OpenAsync().ConfigureAwait(false);

				var aCommand = new NpgsqlCommand(
					"Insert into rentalagreement (customerid, locationid, renterid, additionaldrivers, outdate, indate, automobileid, additions, status, employeeid) VALUES (:value1, :value2, :value3, :value4, :value5, :value6, :value7, :value8, :value9, :value10) RETURNING id", Connection);
				aCommand.Parameters.AddWithValue("value1", theRentalAgreement.Customer);
				aCommand.Parameters.AddWithValue("value2", theRentalAgreement.Location);
				aCommand.Parameters.AddWithValue("value3", theRentalAgreement.Renter);
				aCommand.Parameters.AddWithValue("value4",
					theRentalAgreement.AdditionalDrivers.Any() ? string.Join(";;", theRentalAgreement.AdditionalDrivers) : string.Empty);
				aCommand.Parameters.AddWithValue("value5", theRentalAgreement.OutDate);
				aCommand.Parameters.AddWithValue("value6", theRentalAgreement.InDate);
				aCommand.Parameters.AddWithValue("value7", theRentalAgreement.Automobile);
				aCommand.Parameters.AddWithValue("value8",
					theRentalAgreement.Additions.Any() ? string.Join(";;", theRentalAgreement.Additions) : string.Empty);
				aCommand.Parameters.AddWithValue("value9", theRentalAgreement.Status);
				aCommand.Parameters.AddWithValue("value10", theRentalAgreement.EmployeeId);

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

		public async Task UpdateRentalAgreementAsync(RentalAgreement theRentalAgreement)
		{
			try
			{
				await Connection.OpenAsync().ConfigureAwait(false);

				var aCommand = new NpgsqlCommand(
					"UPDATE rentalagreement SET customerid = :value1, locationid = :value2, renterid = :value3, additionaldrivers = :value4, outdate = :value5, indate = :value6, automobileid = :value7, additions = :value8, status = :value9, employeeid = :value10 where id=:value11;", Connection);
				aCommand.Parameters.AddWithValue("value1", theRentalAgreement.Customer);
				aCommand.Parameters.AddWithValue("value2", theRentalAgreement.Location);
				aCommand.Parameters.AddWithValue("value3", theRentalAgreement.Renter);
				aCommand.Parameters.AddWithValue("value4",
					theRentalAgreement.AdditionalDrivers.Any() ? string.Join(";;", theRentalAgreement.AdditionalDrivers) : string.Empty);
				aCommand.Parameters.AddWithValue("value5", theRentalAgreement.OutDate);
				aCommand.Parameters.AddWithValue("value6", theRentalAgreement.InDate);
				aCommand.Parameters.AddWithValue("value7", theRentalAgreement.Automobile);
				aCommand.Parameters.AddWithValue("value8",
					theRentalAgreement.Additions.Any() ? string.Join(";;", theRentalAgreement.Additions) : string.Empty);
				aCommand.Parameters.AddWithValue("value9", theRentalAgreement.Status);
				aCommand.Parameters.AddWithValue("value10", theRentalAgreement.EmployeeId);
				aCommand.Parameters.AddWithValue("value11", theRentalAgreement.Id);

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

		public async Task DeleteRentalAgreementAsync(RentalAgreement theRentalAgreement)
		{
			try
			{
				await Connection.OpenAsync().ConfigureAwait(false);

				var aCommand = new NpgsqlCommand("DELETE from rentalagreement where id=:value1", Connection);
				aCommand.Parameters.AddWithValue("value1", theRentalAgreement.Id);

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

		public async Task<IEnumerable<RentalAgreement>> GetRentalAgreementsForCustomerAsync(int theCustomerId)
		{
			try
			{
				await Connection.OpenAsync().ConfigureAwait(false);

				var aPreparedCommand =
					new NpgsqlCommand(
						"SELECT id, customerid, locationid, renterid, additionaldrivers, outdate, indate, automobileid, additions, status, employeeid from rentalagreement where customerid = :value1", Connection);
				var aParam = new NpgsqlParameter("value1", NpgsqlDbType.Integer) { Value = theCustomerId };
				aPreparedCommand.Parameters.Add(aParam);

				var aReader = await aPreparedCommand.ExecuteReaderAsync().ConfigureAwait(false);

				if (!aReader.HasRows)
					return null;

				var aReturn = new List<RentalAgreement>();
				while (await aReader.ReadAsync().ConfigureAwait(false))
				{
					aReturn.Add(ReadRentalAgreement(aReader));
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


		private static RentalAgreement ReadRentalAgreement(IDataRecord aReader)
		{
			var aAdditionalDriversString = aReader["additionaldrivers"] != DBNull.Value
				? Convert.ToString(aReader["additionaldrivers"]).Split(';', ';')
				: Enumerable.Empty<string>();

			var aAdditions = aReader["additions"] != DBNull.Value
				? Convert.ToString(aReader["additions"]).Split(';', ';')
				: Enumerable.Empty<string>();

			var aReturn = new RentalAgreement
			{
				Id = Convert.ToInt32(aReader["id"]),
				Customer = Convert.ToInt32(aReader["customerid"]),
				Location = Convert.ToInt32(aReader["locationid"]),
				Renter = Convert.ToInt32(aReader["renterid"]),
				AdditionalDrivers = aAdditionalDriversString.Select(aItem => Convert.ToInt32(aItem)).ToList(),
				Additions = aAdditions.ToList(),
				Automobile = Convert.ToInt32(aReader["automobileid"]),
				InDate = Convert.ToDateTime(aReader["indate"]),
				OutDate = Convert.ToDateTime(aReader["outdate"]),
				Status = Convert.ToString(aReader["status"]) ,
				EmployeeId = Convert.ToInt32(aReader["employeeid"])
			};

			return aReturn;
		}
	}
}
