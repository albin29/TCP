using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class Client
{
    static async Task Main()
    {
        IPAddress ipAddress = IPAddress.Parse("ipDuSkaConnecta");
        int port = 27015;

        TcpClient client = new TcpClient();
        await client.ConnectAsync(ipAddress, port);

        try
        {
            using NetworkStream stream = client.GetStream();

            while (true)
            {
                Console.Write("Enter a message to send to the server (or type 'exit' to quit): ");
                string message = Console.ReadLine();
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                await stream.WriteAsync(buffer, 0, buffer.Length);

                Console.WriteLine("Message sent to server");
            }
        }
        finally
        {
            client.Close();
        }
    }
}
