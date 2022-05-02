

using System.Net;
using System.Net.Sockets;

public class AgarioServer
{
    
    static void Main()
    {
        var endpoint = new IPEndPoint(IPAddress.Loopback, 1313);

        var tcpListener = new TcpListener(endpoint);
        tcpListener.Start();

        ConnectToServer();



    }

    public static async Task ConnectToServer()
    {
        while (true)
        {
            
        }
    }
}

