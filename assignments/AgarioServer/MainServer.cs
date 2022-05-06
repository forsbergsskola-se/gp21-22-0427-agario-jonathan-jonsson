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
            await SendWelcomeResponse(playerConnection);
            Console.WriteLine("Send Welcome message - Done");
            await SendServerID(playerConnection);
            Console.WriteLine("Send assigned ID - Done");
        }

    }
     
    public static async Task SendWelcomeResponse(Connection playerConnection)
    {
        var newStringMessage = new StringMessage()
        {
            messageName = MessagesEnum.StringMessage,
            stringText =
                $"Welcome to the server, {playerConnection.playerState.playerName}. You have been assigned ID: {playerConnection.playerState.PlayerServerId}"

        };
        
        await MessageHandler.SendMessageAsync(newStringMessage, playerConnection.streamWriter);
         
        
    }

    public static async Task SendServerID(Connection playerConnection)
    {
        var serverIDmessage = new ServerIDAssignmentMessage
        {
            messageName = MessagesEnum.ServerIDAssignmentMessage,
            ID = playerConnection.playerState.PlayerServerId
        };
        await MessageHandler.SendMessageAsync(serverIDmessage, playerConnection.streamWriter);
 
    }


}

