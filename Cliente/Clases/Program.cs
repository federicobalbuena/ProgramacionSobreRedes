using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Servidor;

class Program
{
    public static string serverIp = "127.0.0.1";
    public static int serverPort = 8080;

    public static TcpClient client = null;

    static void Main()
    {
        while (true)
        {
            if (Servidor.ServicesDB.iniciarSesion())
            {
                conectarServer();
                break;
            }
        }
    }

    static void conectarServer()
    {
        IPHostEntry host;
        IPAddress addr;
        IPEndPoint endPoint;
        Socket socket;

        try
        {
            host = Dns.GetHostEntry("localhost");
            addr = host.AddressList[0];
            endPoint = new IPEndPoint(addr, 8080);
            socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            //Se conecta al socket
            socket.Connect(endPoint);

            subirArchivo(socket);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            Console.ReadKey();
        }
    }

    static void subirArchivo(Socket client)
    {
        NetworkStream stream = new NetworkStream(client, true);

        Console.Write("Ingrese el nombre del archivo a enviar: ");
        string fileNameToSend = Console.ReadLine();

        try
        {
            byte[] fileNameData = Encoding.ASCII.GetBytes(fileNameToSend);
            stream.Write(fileNameData, 0, fileNameData.Length);

            using (FileStream fileStream = File.OpenRead(fileNameToSend))
            {
                byte[] buffer = new byte[1024];
                int bytesRead;

                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    stream.Write(buffer, 0, bytesRead);
                }
                Console.WriteLine("EL archivo paso ok");
            }

            stream.Close();

            Console.WriteLine("Quere volver a pasar un archivo? (y/n)");
            var ingreso = Console.ReadLine();

            if (ingreso == "yes" || ingreso == "YES" || ingreso == "y")
            {
                conectarServer();
            }
            else
            {
                Console.WriteLine("Se cierra la conexión con el servidor.");
            }

            Console.ReadLine();
        }
        catch (Exception e)
        {
            Servidor.Utils.printClassError("Program", e);
        }

    }
}
