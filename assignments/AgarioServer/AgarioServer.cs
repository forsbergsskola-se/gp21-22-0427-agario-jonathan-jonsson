

using System.Net;
using System.Net.Sockets;
using System.Text;

public class AgarioServer
{
    private static TcpClient tcpClient;
    static async Task Main()
    {
        var endpoint = new IPEndPoint(IPAddress.Loopback, 1313);

        var tcpListener = new TcpListener(endpoint);
        tcpListener.Start();

        while (true)
        {
            tcpClient = await tcpListener.AcceptTcpClientAsync();
            byte[] serverInput = new byte[100];
             await tcpClient.GetStream().ReadAsync(serverInput, 0, 100);
            Console.WriteLine($"Connected to game server: {Encoding.ASCII.GetString(serverInput)}");
            
        }

    }

    public async Task Disconnect()
    {
        
    }
 
}

