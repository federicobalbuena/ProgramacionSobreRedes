using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace Servidor
{
    internal class Utils
    {
        static string separator = "---------------------------------------------------------------------------------------";
        public static void printSeparator()
        {
            Console.WriteLine("---------------------------------------------------------------------------------------");
        }
        public static void printClassError(string className, Exception error)
        {
            string classError = className;
            string errorMessage = "Error: " + error.Message.ToString();
            Console.WriteLine("");
            printSeparator();
            Console.WriteLine(classError);
            Console.WriteLine(errorMessage);
            printSeparator();
            Console.WriteLine("");
            string[] log = new[] { separator, classError, errorMessage, separator };
            Logger.writeLog(log);
            Console.ReadKey();
        }
    }

}
