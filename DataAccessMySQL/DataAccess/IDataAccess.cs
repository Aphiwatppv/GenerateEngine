
namespace DataAccessMySQL.DataAccess
{
    public interface IDataAccess
    {
        Task ExecuteAsync<U>(string storedProcedures, U parameters);
        Task<IEnumerable<T>> QueryRecordsAsync<T, U>(string storedProcedures, U parameters);
        Task<T> QuerySingleRecordsAsync<T, U>(string storedProcedures, U parameters);
    }
}