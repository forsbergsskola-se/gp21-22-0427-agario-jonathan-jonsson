using System.Net;
using System.Net.Sockets;
using AgarioServer;

public class ServerInit
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
           var playerInit = new Task(()=> playerConnection.Init(tcpClient));
           playerInit.Start();
           // var serverWelcomeResponse = new Task()


        }

    }
    
    public async Task SendWelcomeResponse(Connection playerConnection)
    {
        playerConnection.SendMessage(new StringMessage()
        {
             messageName = MessagesEnum.StringMessage,
             stringText = $"Welcome to the server, {playerConnection.playerName}"
        });
 
    }

}

