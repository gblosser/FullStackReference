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
			using (var aConnection = GetConnection())
			{
				aConnection.Open();
				using (var aPreparedCommand = new NpgsqlCommand())
				{
					aPreparedCommand.Connection = aConnection;
					try
					{
						aPreparedCommand.CommandText =
							"SELECT id, customerid, locationid, renterid, additionaldrivers, outdate, indate, automobileid, additions, status, employeeid from rentalagreement where id = :value1";
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
				}
			}
		}

		public int AddRentalAgreement(RentalAgreement theRentalAgreement)
		{
			using (var aConnection = GetConnection())
			{
				aConnection.Open();
				using (var aCommand = new NpgsqlCommand())
				{
					aCommand.Connection = aConnection;

					var aAdditionalDrivers = theRentalAgreement.AdditionalDrivers;
					var aAdditions = theRentalAgreement.Additions;

					try
					{
						aCommand.CommandText =
							"Insert into rentalagreement (customerid, locationid, renterid, additionaldrivers, outdate, indate, automobileid, additions, status, employeeid) VALUES (:value1, :value2, :value3, :value4, :value5, :value6, :value7, :value8, :value9, :value10) RETURNING id";
						aCommand.Parameters.AddWithValue("value1", theRentalAgreement.Customer);
						aCommand.Parameters.AddWithValue("value2", theRentalAgreement.Location);
						aCommand.Parameters.AddWithValue("value3", theRentalAgreement.Renter);

						if (aAdditionalDrivers != null)
							aCommand.Parameters.AddWithValue("value4", string.Join(";;", theRentalAgreement.AdditionalDrivers));
						else
							aCommand.Parameters.AddWithValue("value4", DBNull.Value);

						aCommand.Parameters.AddWithValue("value5", theRentalAgreement.OutDate);
						aCommand.Parameters.AddWithValue("value6", theRentalAgreement.InDate);
						aCommand.Parameters.AddWithValue("value7", theRentalAgreement.Automobile);

						if (aAdditions != null)
						{
							aCommand.Parameters.AddWithValue("value8", string.Join(";;", theRentalAgreement.Additions));
						}
						else
						{
							aCommand.Parameters.AddWithValue("value8", DBNull.Value);
						}

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
				}
			}
		}

		public void UpdateRentalAgreement(RentalAgreement theRentalAgreement)
		{
			using (var aConnection = GetConnection())
			{
				aConnection.Open();
				using (var aCommand = new NpgsqlCommand())
				{
					aCommand.Connection = aConnection;

					var aAdditionalDrivers = theRentalAgreement.AdditionalDrivers;
					var aAdditions = theRentalAgreement.Additions;

					aCommand.CommandText =
						"UPDATE rentalagreement SET customerid = :value1, locationid = :value2, renterid = :value3, additionaldrivers = :value4, outdate = :value5, indate = :value6, automobileid = :value7, additions = :value8, status = :value9, employeeid = :value10 where id=:value11;";
					aCommand.Parameters.AddWithValue("value1", theRentalAgreement.Customer);
					aCommand.Parameters.AddWithValue("value2", theRentalAgreement.Location);
					aCommand.Parameters.AddWithValue("value3", theRentalAgreement.Renter);

					if (aAdditionalDrivers != null)
						aCommand.Parameters.AddWithValue("value4", string.Join(";;", theRentalAgreement.AdditionalDrivers));
					else
						aCommand.Parameters.AddWithValue("value4", DBNull.Value);

					aCommand.Parameters.AddWithValue("value5", theRentalAgreement.OutDate);
					aCommand.Parameters.AddWithValue("value6", theRentalAgreement.InDate);
					aCommand.Parameters.AddWithValue("value7", theRentalAgreement.Automobile);

					if (aAdditions != null)
					{
						aCommand.Parameters.AddWithValue("value8", string.Join(";;", theRentalAgreement.Additions));
					}
					else
					{
						aCommand.Parameters.AddWithValue("value8", DBNull.Value);
					}

					aCommand.Parameters.AddWithValue("value9", theRentalAgreement.Status);
					aCommand.Parameters.AddWithValue("value10", theRentalAgreement.EmployeeId);
					aCommand.Parameters.AddWithValue("value11", theRentalAgreement.Id);

					aCommand.ExecuteNonQuery();
				}
			}
		}

		public void DeleteRentalAgreement(RentalAgreement theRentalAgreement)
		{
			using (var aConnection = GetConnection())
			{
				aConnection.Open();
				using (var aCommand = new NpgsqlCommand())
				{
					aCommand.Connection = aConnection;
					aCommand.CommandText = "DELETE from rentalagreement where id=:value1";
					aCommand.Parameters.AddWithValue("value1", theRentalAgreement.Id);

					aCommand.ExecuteNonQuery(); GetConnection().Close();
				}
			}
		}

		public async Task<RentalAgreement> GetRentalAgreementAsync(int theRentalAgreementId)
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
							"SELECT id, customerid, locationid, renterid, additionaldrivers, outdate, indate, automobileid, additions, status, employeeid from rentalagreement where id = :value1";
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
				}
			}
		}

		public async Task<int> AddRentalAgreementAsync(RentalAgreement theRentalAgreement)
		{
			using (var aConnection = GetConnection())
			{
				aConnection.Open();
				using (var aCommand = new NpgsqlCommand())
				{
					aCommand.Connection = aConnection;

					try
					{
						var aAdditionalDrivers = theRentalAgreement.AdditionalDrivers;
						var aAdditions = theRentalAgreement.Additions;

						aCommand.CommandText =
							"Insert into rentalagreement (customerid, locationid, renterid, additionaldrivers, outdate, indate, automobileid, additions, status, employeeid) VALUES (:value1, :value2, :value3, :value4, :value5, :value6, :value7, :value8, :value9, :value10) RETURNING id";
						aCommand.Parameters.AddWithValue("value1", theRentalAgreement.Customer);
						aCommand.Parameters.AddWithValue("value2", theRentalAgreement.Location);
						aCommand.Parameters.AddWithValue("value3", theRentalAgreement.Renter);

						if (aAdditionalDrivers != null)
							aCommand.Parameters.AddWithValue("value4", string.Join(";;", theRentalAgreement.AdditionalDrivers));
						else
							aCommand.Parameters.AddWithValue("value4", DBNull.Value);

						aCommand.Parameters.AddWithValue("value5", theRentalAgreement.OutDate);
						aCommand.Parameters.AddWithValue("value6", theRentalAgreement.InDate);
						aCommand.Parameters.AddWithValue("value7", theRentalAgreement.Automobile);

						if (aAdditions != null)
						{
							aCommand.Parameters.AddWithValue("value8", string.Join(";;", theRentalAgreement.Additions));
						}
						else
						{
							aCommand.Parameters.AddWithValue("value8", DBNull.Value);
						}

						aCommand.Parameters.AddWithValue("value9", theRentalAgreement.Status);
						aCommand.Parameters.AddWithValue("value10", theRentalAgreement.EmployeeId);

						// returns the id from the SELECT, RETURNING sql statement above
						return Convert.ToInt32(await aCommand.ExecuteScalarAsync().ConfigureAwait(false));
					}
					catch (NpgsqlException ex)
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

		public async Task UpdateRentalAgreementAsync(RentalAgreement theRentalAgreement)
		{
			using (var aConnection = GetConnection())
			{
				await aConnection.OpenAsync();
				using (var aCommand = new NpgsqlCommand())
				{
					aCommand.Connection = aConnection;
					var aAdditionalDrivers = theRentalAgreement.AdditionalDrivers;
					var aAdditions = theRentalAgreement.Additions;

					aCommand.CommandText =
						"UPDATE rentalagreement SET customerid = :value1, locationid = :value2, renterid = :value3, additionaldrivers = :value4, outdate = :value5, indate = :value6, automobileid = :value7, additions = :value8, status = :value9, employeeid = :value10 where id=:value11;";
					aCommand.Parameters.AddWithValue("value1", theRentalAgreement.Customer);
					aCommand.Parameters.AddWithValue("value2", theRentalAgreement.Location);
					aCommand.Parameters.AddWithValue("value3", theRentalAgreement.Renter);

					if (aAdditionalDrivers != null)
						aCommand.Parameters.AddWithValue("value4", string.Join(";;", theRentalAgreement.AdditionalDrivers));
					else
						aCommand.Parameters.AddWithValue("value4", DBNull.Value);

					aCommand.Parameters.AddWithValue("value5", theRentalAgreement.OutDate);
					aCommand.Parameters.AddWithValue("value6", theRentalAgreement.InDate);
					aCommand.Parameters.AddWithValue("value7", theRentalAgreement.Automobile);

					if (aAdditions != null)
					{
						aCommand.Parameters.AddWithValue("value8", string.Join(";;", theRentalAgreement.Additions));
					}
					else
					{
						aCommand.Parameters.AddWithValue("value8", DBNull.Value);
					}

					aCommand.Parameters.AddWithValue("value9", theRentalAgreement.Status);
					aCommand.Parameters.AddWithValue("value10", theRentalAgreement.EmployeeId);
					aCommand.Parameters.AddWithValue("value11", theRentalAgreement.Id);

					await aCommand.ExecuteNonQueryAsync().ConfigureAwait(false);
				}
			}
		}

		public async Task DeleteRentalAgreementAsync(RentalAgreement theRentalAgreement)
		{
			using (var aConnection = GetConnection())
			{
				await aConnection.OpenAsync();
				using (var aCommand = new NpgsqlCommand())
				{
					aCommand.Connection = aConnection;
					aCommand.CommandText = "DELETE from rentalagreement where id=:value1";
					aCommand.Parameters.AddWithValue("value1", theRentalAgreement.Id);

					await aCommand.ExecuteNonQueryAsync().ConfigureAwait(false);
				}
			}
		}

		public async Task<IEnumerable<RentalAgreement>> GetRentalAgreementsForCustomerAsync(int theCustomerId)
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
							"SELECT id, customerid, locationid, renterid, additionaldrivers, outdate, indate, automobileid, additions, status, employeeid from rentalagreement where customerid = :value1";
						var aParam = new NpgsqlParameter("value1", NpgsqlDbType.Integer) { Value = theCustomerId };
						aPreparedCommand.Parameters.Add(aParam);

						var aReader = await aPreparedCommand.ExecuteReaderAsync().ConfigureAwait(false);

						if (!aReader.HasRows)
							return Enumerable.Empty<RentalAgreement>();

						var aReturn = new List<RentalAgreement>();
						while (await aReader.ReadAsync().ConfigureAwait(false))
						{
							aReturn.Add(ReadRentalAgreement(aReader));
						}
						return aReturn;
					}
					catch (NpgsqlException)
					{
						return Enumerable.Empty<RentalAgreement>();
					}
					catch (InvalidOperationException)
					{
						return Enumerable.Empty<RentalAgreement>();
					}
					catch (SqlException)
					{
						return Enumerable.Empty<RentalAgreement>();
					}
					catch (ConfigurationErrorsException)
					{
						return Enumerable.Empty<RentalAgreement>();
					}
					catch (Exception ex)
					{
						string mess = ex.Message;
						return Enumerable.Empty<RentalAgreement>();
					}
				}
			}
		}


		private static RentalAgreement ReadRentalAgreement(IDataRecord aReader)
		{
			var aAdditionalDriversReader = Convert.ToString(aReader["additionaldrivers"]);

			string[] aAdditionalDriversString;
			if (aReader["additionaldrivers"] != DBNull.Value && aAdditionalDriversReader != string.Empty)
			{
				aAdditionalDriversString = Convert.ToString(aReader["additionaldrivers"]).Split(';', ';');
			}
			else
			{
				aAdditionalDriversString = Enumerable.Empty<string>().ToArray();
			}

			var aAdditions = (aReader["additions"] != DBNull.Value && Convert.ToString(aReader["additions"]) != string.Empty)
				? Convert.ToString(aReader["additions"]).Split(';', ';')
				: Enumerable.Empty<string>().ToArray();

			var aIndate = Convert.ToDateTime(aReader["indate"]);

			var aReturn = new RentalAgreement
			{
				Id = Convert.ToInt32(aReader["id"]),
				Customer = Convert.ToInt32(aReader["customerid"]),
				Location = Convert.ToInt32(aReader["locationid"]),
				Renter = Convert.ToInt32(aReader["renterid"]),
				AdditionalDrivers = aAdditionalDriversString.Any() ? aAdditionalDriversString.Select(aItem => Convert.ToInt32(aItem)).ToList() : new List<int>(),
				Additions = aAdditions.ToList(),
				Automobile = Convert.ToInt32(aReader["automobileid"]),
				InDate = aIndate,
				OutDate = Convert.ToDateTime(aReader["outdate"]),
				Status = Convert.ToString(aReader["status"]),
				EmployeeId = Convert.ToInt32(aReader["employeeid"])
			};

			return aReturn;
		}
	}
}
