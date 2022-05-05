using System.Net;
using System.Net.Sockets;
using AgarioServer;

public class MainServer //TODO: name??? atm only entry point for player connection and starting player init
{
    private static TcpClient tcpClient; 
    
    static async Task Main()
    {
        var endpoint = new IPEndPoint(IPAddress.Loopback, 1313);
        var tcpListener = new TcpListener(endpoint);
        tcpListener.Start();
        
        while (true)
        {
            Console.WriteLine("Awaiting connection...");        
            tcpClient =  await tcpListener.AcceptTcpClientAsync();
            var playerConnection =  new Connection();
            await  playerConnection.Init(tcpClient);


        }

    }


}

