using System.Net;
using System.Net.Sockets;
using AgarioServer;

public class MainServer //TODO: name??? atm only entry point for player connection and starting player init
{
    private static PlayerClient playerClient;
    private static int id;
    private static async Task Main()
    {
        var endpoint = new IPEndPoint(IPAddress.Loopback, 1313);
        var tcpListener = new TcpListener(endpoint);
        tcpListener.Start();

        while (true)
        {
            Console.WriteLine("Awaiting connection...");
            var tcpClient = await tcpListener.AcceptTcpClientAsync();
            
            await SetUpPlayerClient(tcpClient);
        }
    }

    private static async Task SetUpPlayerClient(TcpClient tcpClient)
    {
        playerClient = new PlayerClient()
        {
            PlayerServerId = ++id,
            PlayerState = new PlayerState(),
            StreamWriter = new StreamWriter(tcpClient.GetStream())
            {
                AutoFlush = true
            }
        };
        playerClient.PlayerTcpClient = tcpClient;

        new Task(() => MessageHandler.ReadMessage(playerClient)).Start();

        await SendWelcomeResponseandID(playerClient);
        Console.WriteLine("Send welcome response and ID - Done");

        await AssignRandomStartPosition(playerClient);

        new Task(() => ContinuousBroadCaster(playerClient)).Start();
    }

    private static async Task ContinuousBroadCaster(PlayerClient playerClient)
    {
        while (true)
        {
            await ServerDataPackages.SendServerDataPackages(playerClient);

            Thread.Sleep(15); // time between each broadcast
        }
    }

    private static async Task AssignRandomStartPosition(PlayerClient playerClient)
    {
        var random = new Random();

        var randomStartPos = new Vector2Message
        {
            MessageName = MessagesEnum.Vector2Message,
            X = random.Next(-GameState.BoardSizeX / 2, GameState.BoardSizeX / 2),
            Y = random.Next(-GameState.BoardSizeY / 2, GameState.BoardSizeY / 2)
        };

        await MessageHandler.SendMessageAsync(randomStartPos, playerClient.StreamWriter);
    }

    private static async Task SendWelcomeResponseandID(PlayerClient playerClient)
    {
        var newStringMessage = new StringMessage
        {
            MessageName = MessagesEnum.StringMessage,
            StringText =
                $"Welcome to the server! You have been assigned ID: {playerClient.PlayerServerId}"
        };
        var serverIDMessage = new ServerIDAssignmentMessage
        {
            MessageName = MessagesEnum.ServerIdAssignmentMessage,
            Id = playerClient.PlayerServerId
        };
        await MessageHandler.SendMessageAsync(newStringMessage, playerClient.StreamWriter);
        await MessageHandler.SendMessageAsync(serverIDMessage, playerClient.StreamWriter);
    }
}