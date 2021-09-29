using System;
using Serilog;

namespace UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.Console().WriteTo.File("../Logs/logs.txt", rollingInterval: RollingInterval.Day).CreateLogger();
            Log.Information("Application starting..");
            new MainMenu().start();
            Log.Information("Application closing..");
        }
    }
}
