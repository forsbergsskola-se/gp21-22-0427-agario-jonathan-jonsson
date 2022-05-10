using System.Net;
using System.Net.Sockets;
using AgarioServer.Model;
using Assets.Scripts.AgarioShared.Network;
using Assets.Scripts.AgarioShared.Network.Messages;

namespace AgarioServer.Network;

public class MainServer
{
    //Dictionary<Id,playerClient>
    private static Dictionary<int, PlayerClient> connectedPlayerClients;
    private static PlayerClient playerClient;
    private static int id { get; set; }
    private static async Task Main()
    {
        var endpoint = new IPEndPoint(IPAddress.Loopback, 1313);
        var tcpListener = new TcpListener(endpoint);
        tcpListener.Start();
        connectedPlayerClients = new Dictionary<int, PlayerClient>();
        while (true)
        {
            Console.WriteLine("Awaiting connection...");
            var tcpClient = await tcpListener.AcceptTcpClientAsync();
            
            await SetUpPlayerClient(tcpClient);
        }
    }
    
 
    private static async Task SetUpPlayerClient(TcpClient tcpClient)
    {
        playerClient = new PlayerClient(tcpClient,++id);

        connectedPlayerClients.Add(id,playerClient);

        await SendWelcomeResponseandID(playerClient);
        Console.WriteLine("Send welcome response and ID - Done");
        new Task(() => OrbTicker(playerClient)).Start();
        new Task(() => ContinuousBroadCaster(playerClient)).Start();
    }

    private static async Task OrbTicker(PlayerClient playerClient)
    {
        while (true)
        {
            await OrbSpawner.SpawnOrb(playerClient);
            await Task.Delay(3000);
        }
    }
    
    
    private static async Task ContinuousBroadCaster(PlayerClient playerClient)
    {
        while (true)
        {
            await ServerDataPackages.SendServerDataPackages(playerClient);
            await Task.Delay(15);
        }
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