using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Servidor
{
    internal class Logger
    {
        public static void writeLog(string[] inputData)
        {
            string fileName = "log_server.txt";

            string[] lines = inputData;

            int bufferSize = 1024;

            try
            {
                using (FileStream fileStream = new FileStream(fileName, FileMode.Append))
                using (BufferedStream bufferedStream = new BufferedStream(fileStream, bufferSize))
                using (StreamWriter writer = new StreamWriter(bufferedStream))
                {
                    foreach (string line in lines)
                    {
                        writer.WriteLine(line);
                    }

                    writer.Close();
                }

            }
            catch (IOException e)
            {
                Console.WriteLine("Error al escribir en el archivo: " + e.Message);
            }
        }
    }
}
