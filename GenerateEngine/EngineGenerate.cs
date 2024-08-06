using DataAccessMySQL.DataAccess;
using DataAccessMySQL.Model;
using DataAccessMySQL.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenerateEngine
{
    public class EngineGenerate
    {
        private readonly string connectionstring = "server=localhost;uid=root;pwd=!Aphiwat33110!;database=rtms_db;SslMode=None;";
        private readonly IRepository repository;
        public EngineGenerate()
        {
            repository = new Repository(new DataAccess(connectionstring));
        }

        public async void GenerateMachineInfo()
        {

            int iterationCount = 100;

            for (int i = 0; i < iterationCount; i++)
            {
                var machinelist = await repository.RepoGetAllMachinesInfo();
                string _machineName = GenerateMachineString(machinelist);

                await repository.AddNewMachineInfo(_machineName);

                int percentage = (i + 1) * 100 / iterationCount;
                Console.Write($"\rProgress: {percentage}% {_machineName}");
            }
            Console.WriteLine("\nEnd Generate Machine Info");

        }

        public async void GenerateMachineData()
        {
            var resultMachine = await repository.RepoGetAllMachinesInfo();
            int iterationCount = 100;
            {
                for (int i = 0; i < iterationCount; i++)
                {

                    // string _machineName = RandomMachineFromList(resultMachine);
                    int machine_id = RandomIdFromList(resultMachine);

                    var inputmachine = new InputMachineData
                    {
                        MachineId = machine_id,
                        Current = GenerateRandomDouble(0.5, 20),
                        Vibration = GenerateRandomDouble(0.1, 15),
                        Voltage = GenerateRandomDouble(110, 220),


                    };

                    await repository.AddNewMachineData(inputmachine);
                    int percentage = (i + 1) * 100 / iterationCount;
                    Console.Write($"\rProgress: {percentage}% MachineId :{machine_id} : Voltage : {inputmachine.Voltage} || Current: {inputmachine.Current} || Vibration : {inputmachine.Vibration}");
           

                }
                Console.WriteLine("\nEnd Generate Machine Data");

            }

        }


        private string GenerateMachineString(IEnumerable<MachineInfo> machineInfos)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var existingNames = new HashSet<string>(machineInfos.Select(m => m.Machine_name));
            string newName;

            do
            {
                StringBuilder result = new StringBuilder("machine_");
                for (int i = 0; i < 10; i++)
                {
                    result.Append(chars[random.Next(chars.Length)]);
                }
                newName = result.ToString();
            } while (existingNames.Contains(newName));

            return newName;
        }

        private string RandomMachineFromList(IEnumerable<MachineInfo> machineInfos)
        {
            Random random = new Random();
            // Convert the IEnumerable to a List to easily access elements by index
            var machineList = machineInfos.ToList();

            if (!machineList.Any())
            {
                throw new InvalidOperationException("The list of machineInfos is empty.");
            }

            // Generate a random index within the range of the list
            int randomIndex = random.Next(machineList.Count);

            // Return the name of the randomly selected MachineInfo
            return machineList[randomIndex].Machine_name;
        }

        private int RandomIdFromList(IEnumerable<MachineInfo> machineInfos)
        {
            Random random = new Random();
            var machineList = machineInfos.ToList();

            if (!machineList.Any())
            {
                throw new InvalidOperationException("The list of machineInfos is empty.");
            }

            int randomIndex = random.Next(machineList.Count);
            return machineList[randomIndex].Id_machine;


        }

       

        private double GenerateRandomDouble(double lowerLimit, double upperLimit)
        {
             Random random = new Random();
            if (lowerLimit >= upperLimit)
            {
                throw new ArgumentException("Lower limit must be less than upper limit.");
            }

            return random.NextDouble() * (upperLimit - lowerLimit) + lowerLimit;
        }
    }
}
