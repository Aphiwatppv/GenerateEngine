using DataAccessMySQL.DataAccess;
using DataAccessMySQL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logger;
using ZstdSharp.Unsafe;

namespace DataAccessMySQL.Repository
{

    public class Repository : IRepository
    {
        private readonly IDataAccess _dataAccess;

        public Repository(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<IEnumerable<MachineInfo>> RepoGetAllMachinesInfo()
        {
            try
            {
                var result = await _dataAccess.QueryRecordsAsync<MachineInfo, dynamic>(storedProcedures: "rtms_db.GetAllMachineInfo", new { });
                return result;
            }
            catch (Exception ex)
            {

                using (LoggerMethod _logger = new LoggerMethod("LogRepositoryError.txt"))
                {
                    _logger.LogError($"RepoGetAllMachinesInfo Error : {ex.Message}");

                    return Enumerable.Empty<MachineInfo>();
                }

            }


        }
        public async Task<IEnumerable<MachineData>> RepoGetAllDataMachine()
        {
            try
            {
                var result = await _dataAccess.QueryRecordsAsync<MachineData, dynamic>(storedProcedures: " rtms_db.GetAllMachineData()", new { });
                return result;
            }
            catch (Exception ex)
            {

                using (LoggerMethod _logger = new LoggerMethod("LogRepositoryError.txt"))
                {
                    _logger.LogError($"RepoGetAllDataMachine Error : {ex.Message}");

                    return Enumerable.Empty<MachineData>();
                }

            }
        }
        public async Task AddNewMachineInfo(string machine_name)
        {
            try
            {
                await _dataAccess.ExecuteAsync(storedProcedures: "rtms_db.AddMachineInfo", new { p_Machine_name = machine_name });
            }
            catch (Exception ex)
            {
                using (LoggerMethod _logger = new LoggerMethod("LogRepositoryError.txt"))
                {
                    _logger.LogError($"AddNewMachineInfo Error : {ex.Message}");


                }
            }
        }
        public async Task AddNewMachineData(InputMachineData input)
        {
            try
            {
                await _dataAccess.ExecuteAsync(storedProcedures: "rtms_db.AddMachineData", new
                {
                    p_Machine_Id = input.MachineId,
                    p_Voltage = input.Voltage,
                    p_Current = input.Current,
                    p_Vibration = input.Vibration,
                });
            }
            catch (Exception ex)
            {
                using (LoggerMethod _logger = new LoggerMethod("LogRepositoryError.txt"))
                {
                    _logger.LogError($"AddNewMachineData Error : {ex.Message}");


                }
            }
        }
    }
}
