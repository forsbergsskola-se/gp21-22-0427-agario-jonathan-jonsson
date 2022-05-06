using System.Net;
using System.Net.Sockets;
using AgarioServer;

public class MainServer //TODO: name??? atm only entry point for player connection and starting player init
{
    
    
    static async Task Main()
    {
        var endpoint = new IPEndPoint(IPAddress.Loopback, 1313);
        var tcpListener = new TcpListener(endpoint);
        tcpListener.Start();
        
        while (true)
        {
            Console.WriteLine("Awaiting connection...");        
            var tcpClient =  await tcpListener.AcceptTcpClientAsync();
            var playerConnection =  new Connection();
            
            await  playerConnection.Init(tcpClient);
 
            //TODO: Name is not set in Init before the send welcome message is sent. Or rather, maybe it sends before receiving from server. How handle?
            await SendWelcomeResponse(playerConnection);
            Console.WriteLine("Send welcome response - Done");
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
                $"Welcome to the server, {playerConnection.playerClient.playerState.playerName}. You have been assigned ID: {playerConnection.playerClient.playerServerId}"

        };
        
        await MessageHandler.SendMessageAsync(newStringMessage, playerConnection.streamWriter);
    }

    public static async Task SendServerID(Connection playerConnection)
    {
        var serverIDMessage = new ServerIDAssignmentMessage
        {
            messageName = MessagesEnum.ServerIdAssignmentMessage,
            ID = playerConnection.playerClient.playerServerId
        };
        await MessageHandler.SendMessageAsync(serverIDMessage, playerConnection.streamWriter);
 
    }


}

