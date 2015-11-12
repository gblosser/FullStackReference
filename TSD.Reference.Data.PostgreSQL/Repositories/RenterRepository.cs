﻿using System;
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
	public class RenterRepository : AbstractRepository, IRenterRepository
	{
		public Renter GetRenter(int theRenterId)
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
							"SELECT id, firstname, lastname, address, city, state, postalcode, country, licensenumber, licensestate, customerid, paymentinfo, insuranceinfo from renter where id = :value1";
						var aParam = new NpgsqlParameter("value1", NpgsqlDbType.Integer) { Value = theRenterId };
						aPreparedCommand.Parameters.Add(aParam);

						var aReader = aPreparedCommand.ExecuteReader();

						if (!aReader.HasRows)
							return null;

						var aReturn = new Renter();
						while (aReader.Read())
						{
							aReturn = ReadRenter(aReader);
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

		public int AddRenter(Renter theRenter)
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
							"Insert into renter (firstname, lastname, address, city, state, postalcode, country, licensenumber, licensestate, customerid, paymentinfo, insuranceinfo) VALUES (:value1, :value2, :value3, :value4, :value5, :value6, :value7, :value8, :value9, :value10, :value11, :value12) RETURNING id";
						aCommand.Parameters.AddWithValue("value1", theRenter.FirstName);
						aCommand.Parameters.AddWithValue("value2", theRenter.LastName);
						aCommand.Parameters.AddWithValue("value3", theRenter.Address);
						aCommand.Parameters.AddWithValue("value4", theRenter.City);
						aCommand.Parameters.AddWithValue("value5", theRenter.State);
						aCommand.Parameters.AddWithValue("value6", theRenter.PostalCode);
						aCommand.Parameters.AddWithValue("value7", theRenter.Country);
						aCommand.Parameters.AddWithValue("value8", theRenter.LicenseNumber);
						aCommand.Parameters.AddWithValue("value9", theRenter.LicenseState);
						aCommand.Parameters.AddWithValue("value10", theRenter.CustomerId);
						aCommand.Parameters.AddWithValue("value11", theRenter.PaymentInfo);
						aCommand.Parameters.AddWithValue("value12", theRenter.InsuranceInfo);

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

		public void UpdateRenter(Renter theRenter)
		{
			using (var aConnection = GetConnection())
			{
				aConnection.Open();
				using (var aCommand = new NpgsqlCommand())
				{
					aCommand.Connection = aConnection;
					aCommand.CommandText =
						"UPDATE renter SET firstname = :value1, lastname = :value2, address = :value3, city = :value4, state = :value5, postalcode = :value6, country = :value7, licensenumber = :value8, licensestate = :value9, customerid = :value10, paymentinfo = :value11, insuranceinfo = :value12 where id=:value13;";
					aCommand.Parameters.AddWithValue("value1", theRenter.FirstName);
					aCommand.Parameters.AddWithValue("value2", theRenter.LastName);
					aCommand.Parameters.AddWithValue("value3", theRenter.Address);
					aCommand.Parameters.AddWithValue("value4", theRenter.City);
					aCommand.Parameters.AddWithValue("value5", theRenter.State);
					aCommand.Parameters.AddWithValue("value6", theRenter.PostalCode);
					aCommand.Parameters.AddWithValue("value7", theRenter.Country);
					aCommand.Parameters.AddWithValue("value8", theRenter.LicenseNumber);
					aCommand.Parameters.AddWithValue("value9", theRenter.LicenseState);
					aCommand.Parameters.AddWithValue("value10", theRenter.CustomerId);
					aCommand.Parameters.AddWithValue("value11", theRenter.PaymentInfo);
					aCommand.Parameters.AddWithValue("value12", theRenter.InsuranceInfo);
					aCommand.Parameters.AddWithValue("value13", theRenter.Id);

					aCommand.ExecuteNonQuery();
				}
			}
		}

		public void DeleteRenter(Renter theRenter)
		{
			using (var aConnection = GetConnection())
			{
				aConnection.Open();
				using (var aCommand = new NpgsqlCommand())
				{
					aCommand.Connection = aConnection;
					aCommand.CommandText = "DELETE from renter where id=:value1";
					aCommand.Parameters.AddWithValue("value1", theRenter.Id);

					aCommand.ExecuteNonQuery();
				}
			}
		}

		public async Task<Renter> GetRenterAsync(int theRenterId)
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
							"SELECT id, firstname, lastname, address, city, state, postalcode, country, licensenumber, licensestate, customerid, paymentinfo, insuranceinfo from renter where id = :value1";
						var aParam = new NpgsqlParameter("value1", NpgsqlDbType.Integer) { Value = theRenterId };
						aPreparedCommand.Parameters.Add(aParam);

						var aReader = await aPreparedCommand.ExecuteReaderAsync();

						if (!aReader.HasRows)
							return null;

						var aReturn = new Renter();
						while (await aReader.ReadAsync())
						{
							aReturn = ReadRenter(aReader);
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

		public async Task<IEnumerable<Renter>> GetRentersForCustomerAsync(int theCustomerId)
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
							"SELECT id, firstname, lastname, address, city, state, postalcode, country, licensenumber, licensestate, customerid, paymentinfo, insuranceinfo from renter where customerid = :value1";
						var aParam = new NpgsqlParameter("value1", NpgsqlDbType.Integer) { Value = theCustomerId };
						aPreparedCommand.Parameters.Add(aParam);

						var aReader = await aPreparedCommand.ExecuteReaderAsync();

						if (!aReader.HasRows)
							return null;

						var aReturn = new List<Renter>();
						while (await aReader.ReadAsync())
						{
							aReturn.Add(ReadRenter(aReader));
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

		public async Task<int> AddRenterAsync(Renter theRenter)
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
							"Insert into renter (firstname, lastname, address, city, state, postalcode, country, licensenumber, licensestate, customerid, paymentinfo, insuranceinfo) VALUES (:value1, :value2, :value3, :value4, :value5, :value6, :value7, :value8, :value9, :value10, :value11, :value12) RETURNING id";
						aCommand.Parameters.AddWithValue("value1", theRenter.FirstName);
						aCommand.Parameters.AddWithValue("value2", theRenter.LastName);
						aCommand.Parameters.AddWithValue("value3", theRenter.Address);
						aCommand.Parameters.AddWithValue("value4", theRenter.City);
						aCommand.Parameters.AddWithValue("value5", theRenter.State);
						aCommand.Parameters.AddWithValue("value6", theRenter.PostalCode);
						aCommand.Parameters.AddWithValue("value7", theRenter.Country);
						aCommand.Parameters.AddWithValue("value8", theRenter.LicenseNumber);
						aCommand.Parameters.AddWithValue("value9", theRenter.LicenseState);
						aCommand.Parameters.AddWithValue("value10", theRenter.CustomerId);
						aCommand.Parameters.AddWithValue("value11", theRenter.PaymentInfo);
						aCommand.Parameters.AddWithValue("value12", theRenter.InsuranceInfo);

						// returns the id from the SELECT, RETURNING sql statement above
						return Convert.ToInt32(await aCommand.ExecuteScalarAsync());
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

		public async Task UpdateRenterAsync(Renter theRenter)
		{
			using (var aConnection = GetConnection())
			{
				await aConnection.OpenAsync();
				using (var aCommand = new NpgsqlCommand())
				{
					aCommand.Connection = aConnection;
					aCommand.CommandText =
						"UPDATE renter SET firstname = :value1, lastname = :value2, address = :value3, city = :value4, state = :value5, postalcode = :value6, country = :value7, licensenumber = :value8, licensestate = :value9, customerid = :value10, paymentinfo = :value11, insuranceinfo = :value12 where id=:value13;";
					aCommand.Parameters.AddWithValue("value1", theRenter.FirstName);
					aCommand.Parameters.AddWithValue("value2", theRenter.LastName);
					aCommand.Parameters.AddWithValue("value3", theRenter.Address);
					aCommand.Parameters.AddWithValue("value4", theRenter.City);
					aCommand.Parameters.AddWithValue("value5", theRenter.State);
					aCommand.Parameters.AddWithValue("value6", theRenter.PostalCode);
					aCommand.Parameters.AddWithValue("value7", theRenter.Country);
					aCommand.Parameters.AddWithValue("value8", theRenter.LicenseNumber);
					aCommand.Parameters.AddWithValue("value9", theRenter.LicenseState);
					aCommand.Parameters.AddWithValue("value10", theRenter.CustomerId);
					aCommand.Parameters.AddWithValue("value11", theRenter.PaymentInfo);
					aCommand.Parameters.AddWithValue("value12", theRenter.InsuranceInfo);
					aCommand.Parameters.AddWithValue("value13", theRenter.Id);

					await aCommand.ExecuteNonQueryAsync();
				}
			}
		}

		public async Task DeleteRenterAsync(Renter theRenter)
		{
			using (var aConnection = GetConnection())
			{
				await aConnection.OpenAsync();
				using (var aCommand = new NpgsqlCommand())
				{
					aCommand.Connection = aConnection;
					aCommand.CommandText = "DELETE from renter where id=:value1";
					aCommand.Parameters.AddWithValue("value1", theRenter.Id);

					await aCommand.ExecuteNonQueryAsync().ConfigureAwait(false);
				}
			}
		}

		private static Renter ReadRenter(IDataRecord aReader)
		{
			var aReturn = new Renter
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
				CustomerId = Convert.ToInt32(aReader["customerid"]),
				PaymentInfo = Convert.ToString(aReader["paymentinfo"]),
				InsuranceInfo = Convert.ToString(aReader["insuranceinfo"])
			};

			return aReturn;
		}
	}
}
