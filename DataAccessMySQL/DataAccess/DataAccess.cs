using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Logger;
using MySql.Data.MySqlClient;
using Dapper;
using System.Data;
using MySqlX.XDevAPI.Common;

namespace DataAccessMySQL.DataAccess
{
    public class DataAccess : IDataAccess
    {
        private readonly string _connectionString;
        public DataAccess(string connectionString)
        {
            _connectionString = connectionString;
            using (LoggerMethod loggerMethod = new LoggerMethod("DataAccessLog.txt"))
            {
                loggerMethod.LogInfo($"Initial connection string : {connectionString}");
            }
        }

        public async Task ExecuteAsync<U>(string storedProcedures, U parameters)
        {
            try
            {
                using (IDbConnection conn = new MySqlConnection(_connectionString))
                {
                    await conn.ExecuteAsync(storedProcedures, parameters);
                }
            }
            catch (Exception ex)
            {
                using (LoggerMethod loggerMethod = new LoggerMethod("DataAccessLog.txt"))
                {
                    loggerMethod.LogError($"Error : {ex.Message}");
                }
                throw;
            }
        }

        public async Task<IEnumerable<T>> QueryRecordsAsync<T, U>(string storedProcedures, U parameters)
        {
            try
            {
                using (IDbConnection conn = new MySqlConnection(_connectionString))
                {
                    var result = await conn.QueryAsync<T>(storedProcedures, parameters);
                    return result;
                }
            }
            catch (Exception ex)
            {
                using (LoggerMethod loggerMethod = new LoggerMethod("DataAccessLog.txt"))
                {
                    loggerMethod.LogError($"Error : {ex.Message}");
                }
                return new List<T>();
            }
        }

        public async Task<T> QuerySingleRecordsAsync<T, U>(string storedProcedures, U parameters)
        {
            try
            {
                using (IDbConnection conn = new MySqlConnection(_connectionString))
                {
                    var result = await conn.QueryFirstAsync<T>(storedProcedures, parameters);
                    return result;
                }
            }
            catch (Exception ex)
            {
                using (LoggerMethod loggerMethod = new LoggerMethod("DataAccessLog.txt"))
                {
                    loggerMethod.LogError($"Error : {ex.Message}");
                }
                return default(T);
            }
        }
    }
}
