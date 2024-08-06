using DataAccessMySQL.Model;

namespace DataAccessMySQL.Repository
{
    public interface IRepository
    {
        Task AddNewMachineData(InputMachineData input);
        Task AddNewMachineInfo(string machine_name);
        Task<IEnumerable<MachineData>> RepoGetAllDataMachine();
        Task<IEnumerable<MachineInfo>> RepoGetAllMachinesInfo();
    }
}