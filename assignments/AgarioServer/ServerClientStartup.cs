using System.Net;
using System.Net.Sockets;
using AgarioServer;

public class ServerClientStartup
{
    private static TcpClient tcpClient; 
    static void Main()
    {
        var endpoint = new IPEndPoint(IPAddress.Loopback, 1313);
        var tcpListener = new TcpListener(endpoint);
        tcpListener.Start();
        
        while (true)
        {
            Console.WriteLine("Awaiting connection...");        
            tcpClient =  tcpListener.AcceptTcpClient();
            var playerConnection =  new Connection(tcpClient);
            
            playerConnection.SendMessage(new TestMessage()
            {
                messageName = "TestMessage",
                testString = $"Welcome to the server, {playerConnection.playerName}"
            });
            
            Console.WriteLine("After message");
        }

    }

}

