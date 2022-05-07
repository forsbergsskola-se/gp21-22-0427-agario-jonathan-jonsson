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
 
            await SendWelcomeResponseandID(playerConnection);
            Console.WriteLine("Send welcome response and ID - Done");
      
            await AssignRandomStartPosition(playerConnection);
            
            new Task(()=>ContinuousBroadCaster(playerConnection)).Start();

        }
    }


    public static async Task ContinuousBroadCaster(Connection playerConnection)
    {
        while (true)
        {
                SendIllegalPositionNotification(playerConnection);
            
            Thread.Sleep(15); // time between each broadcast
        }
    }
    
    
    

    private static async Task AssignRandomStartPosition(Connection playerConnection)
    {
        Random random = new Random();

        var randomStartPos = new Vector2Message()
        {
            messageName = MessagesEnum.Vector2Message,
            x = random.Next(-GameState.boardSizeX/2, GameState.boardSizeX/2),
            y = random.Next(-GameState.boardSizeY/2, GameState.boardSizeY/2)

        };

        await MessageHandler.SendMessageAsync(randomStartPos, playerConnection.streamWriter);
    }

    public static async Task SendWelcomeResponseandID(Connection playerConnection)
    {
        var newStringMessage = new StringMessage()
        {
            messageName = MessagesEnum.StringMessage,
            stringText = $"Welcome to the server! You have been assigned ID: {playerConnection.playerClient.playerServerId}"

        };
        var serverIDMessage = new ServerIDAssignmentMessage
        {
            messageName = MessagesEnum.ServerIdAssignmentMessage,
            ID = playerConnection.playerClient.playerServerId
        };
        await MessageHandler.SendMessageAsync(newStringMessage, playerConnection.streamWriter);
        await MessageHandler.SendMessageAsync(serverIDMessage, playerConnection.streamWriter);

    }

    public static async Task SendIllegalPositionNotification(Connection playerConnection)
    {
        var illegalMovementMessage = new BoolMessage()
        {
            messageName = MessagesEnum.BoolMessage,
            boolValue = playerConnection.playerClient.playerState.IllegalMovement
        };

        var positionCorrection = new Vector2Message()
        {
            messageName = MessagesEnum.Vector2Message,
            x = playerConnection.playerClient.playerState.xPos,
            y = playerConnection.playerClient.playerState.yPos
        };
        await MessageHandler.SendMessageAsync(illegalMovementMessage, playerConnection.streamWriter);
        await MessageHandler.SendMessageAsync(positionCorrection, playerConnection.streamWriter);
    }

}

