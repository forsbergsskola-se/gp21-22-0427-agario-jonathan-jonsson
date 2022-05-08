using System.Net;
using System.Net.Sockets;
using AgarioServer;

public class MainServer //TODO: name??? atm only entry point for player connection and starting player init
{
    private static async Task Main()
    {
        var endpoint = new IPEndPoint(IPAddress.Loopback, 1313);
        var tcpListener = new TcpListener(endpoint);
        tcpListener.Start();

        while (true)
        {
            Console.WriteLine("Awaiting connection...");
            var tcpClient = await tcpListener.AcceptTcpClientAsync();
            var playerConnection = new Connection();

            await playerConnection.Init(tcpClient);

            await SendWelcomeResponseandID(playerConnection);
            Console.WriteLine("Send welcome response and ID - Done");

            await AssignRandomStartPosition(playerConnection);

            new Task(() => ContinuousBroadCaster(playerConnection)).Start();
        }
    }

    private static async Task ContinuousBroadCaster(Connection playerConnection)
    {
        while (true)
        {
            await ServerDataPackages.SendServerDataPackages(playerConnection);

            Thread.Sleep(15); // time between each broadcast
        }
    }

    private static async Task AssignRandomStartPosition(Connection playerConnection)
    {
        var random = new Random();

        var randomStartPos = new Vector2Message
        {
            messageName = MessagesEnum.Vector2Message,
            x = random.Next(-GameState.BoardSizeX / 2, GameState.BoardSizeX / 2),
            y = random.Next(-GameState.BoardSizeY / 2, GameState.BoardSizeY / 2)
        };

        await MessageHandler.SendMessageAsync(randomStartPos, playerConnection.StreamWriter);
    }

    private static async Task SendWelcomeResponseandID(Connection playerConnection)
    {
        var newStringMessage = new StringMessage
        {
            messageName = MessagesEnum.StringMessage,
            stringText =
                $"Welcome to the server! You have been assigned ID: {playerConnection.PlayerClient.PlayerServerId}"
        };
        var serverIDMessage = new ServerIDAssignmentMessage
        {
            messageName = MessagesEnum.ServerIdAssignmentMessage,
            ID = playerConnection.PlayerClient.PlayerServerId
        };
        await MessageHandler.SendMessageAsync(newStringMessage, playerConnection.StreamWriter);
        await MessageHandler.SendMessageAsync(serverIDMessage, playerConnection.StreamWriter);
    }
}