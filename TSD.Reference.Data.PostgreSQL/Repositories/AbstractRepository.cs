using System.Configuration;
using Npgsql;

namespace TSD.Reference.Data.PostgreSQL.Repositories
{
	public class AbstractRepository
	{
		private readonly string connectionString = ConfigurationManager.ConnectionStrings["PostgreSQL"].ConnectionString;
		private NpgsqlConnection _connection = null;

		internal NpgsqlConnection GetConnection() => new NpgsqlConnection(connectionString);
	}
}