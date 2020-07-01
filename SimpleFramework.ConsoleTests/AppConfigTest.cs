using SimpleFramework.Utils.AppConfig;
using System;

namespace SimpleFramework.ConsoleTests
{
    public class AppConfigTest
    {
        public void Tests()
        {
            Console.WriteLine(AppConfigHelper.GetConnectionString("SQLServerConnection1"));
        }
    }
}
