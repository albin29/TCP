using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class Server
{
    static async Task Main()
    {
        IPAddress ipAddress = IPAddress.Parse("0.0.0.0");
        int port = 27015;

        TcpListener listener = new TcpListener(ipAddress, port);

        try
        {
            listener.Start();
            Console.WriteLine($"Server listening on {ipAddress}:{port}");

            TcpClient client = await listener.AcceptTcpClientAsync();
            Console.WriteLine($"Accepted connection from {((IPEndPoint)client.Client.RemoteEndPoint).Address}");

            using NetworkStream stream = client.GetStream();

            while (true)
            {
                byte[] buffer = new byte[1];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

                string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Received message: {receivedMessage}");
            }
        }
        finally
        {
            listener.Stop();
        }
    }
}
