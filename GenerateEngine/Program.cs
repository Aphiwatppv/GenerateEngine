using DataAccessMySQL;
using GenerateEngine;


try
{
    while (true)
    {
        Console.WriteLine("Start Generate data");
        Console.WriteLine("Please select function");
        Console.WriteLine("1 For generate machine info");
        Console.WriteLine("2 For generate machine data");
        Console.WriteLine("3 Exit ");

        if (!int.TryParse(Console.ReadLine(), out int mode) || mode < 1 || mode > 3)
        {
           
            Console.WriteLine("Invalid input. Please select a valid option.");
            continue;
        }

        if (mode == 3)
        {
            Console.WriteLine("Exiting...");
            break;
        }

        switch (mode)
        {
            case 1:
                Console.WriteLine("Start Generate Machine Info");
                EngineGenerate engineGenerate1 = new EngineGenerate();
                engineGenerate1.GenerateMachineInfo();
                break;
            case 2:
                Console.WriteLine("Start Generate Machine Data");
                EngineGenerate engineGenerate2 = new EngineGenerate();
                engineGenerate2.GenerateMachineData();
                break;
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}