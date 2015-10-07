
using SQLite;

namespace TSD.Reference.Data.SQLite.Repositories
{
	/// <summary>
	/// base class that provides connections to the SQLite database for this application
	/// </summary>
	public abstract class AbstractRepository
	{
		internal const string ConnectionString = "tsd.reference.data.db";
		internal static SQLiteConnection Connection => new SQLiteConnection(ConnectionString);
		internal static SQLiteAsyncConnection ConnectionAsync => new SQLiteAsyncConnection(ConnectionString);
	}
}
