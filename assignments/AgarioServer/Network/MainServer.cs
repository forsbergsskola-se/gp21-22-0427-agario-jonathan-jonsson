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
 
            await SendWelcomeResponse(playerConnection);
            Console.WriteLine("Send welcome response - Done");
            await SendServerID(playerConnection);
            Console.WriteLine("Send assigned ID - Done");

            await AssignRandomStartPosition(playerConnection);

        }
    }

    private static async Task AssignRandomStartPosition(Connection playerConnection)
    {
        Random random = new Random();

        var randomStartPos = new Vector2Message()
        {
            messageName = MessagesEnum.Vector2Message,
            x = random.Next(0, GameState.boardSizeX),
            y = random.Next(0, GameState.boardSizeY)

        };

        await MessageHandler.SendMessageAsync(randomStartPos, playerConnection.streamWriter);
    }

    public static async Task SendWelcomeResponse(Connection playerConnection)
    {
        var newStringMessage = new StringMessage()
        {
            messageName = MessagesEnum.StringMessage,
            stringText = $"Welcome to the server! You have been assigned ID: {playerConnection.playerClient.playerServerId}"

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

