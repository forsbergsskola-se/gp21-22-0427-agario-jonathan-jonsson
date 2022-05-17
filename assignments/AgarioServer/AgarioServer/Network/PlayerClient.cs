using System.Net.Sockets;
using AgarioServer.Model;
using Assets.Scripts.AgarioShared.Model;
using Assets.Scripts.AgarioShared.Network;
using Assets.Scripts.AgarioShared.Network.Messages;

namespace AgarioServer.Network;

public class PlayerClient
{
    public PlayerState PlayerState;
    public int PlayerServerId;
    public TcpClient PlayerTcpClient;
    public StreamWriter StreamWriter;

    public PlayerClient(TcpClient tcpClient, int id)
    {
        PlayerServerId = id;
        PlayerTcpClient = tcpClient;
        PlayerState = new PlayerState()
        {
            Size = 1,
            Score = 0,
            PlayerSpeed = 3000
        };
     
        StreamWriter = new StreamWriter(PlayerTcpClient.GetStream())
        {
            AutoFlush = true
        };
        
        new Task(() => MessageHandler.ReadMessage(this)).Start();
        
        DefaultPlayerStateData(this);

    }
    public static async Task DefaultPlayerStateData(PlayerClient playerClient)
    {
        Random random = new Random();
        var defaultDataMessage = new DefaultPlayerStateDataMessage()
        {
            MessageName = MessagesEnum.DefaultPlayerStateDataMessage,
            startXPos = random.Next(-GameState.BoardSizeX / 2, GameState.BoardSizeX / 2),
            startYPos = random.Next(-GameState.BoardSizeY / 2, GameState.BoardSizeY / 2),
            Size = 1,
            Score = 0,
            PlayerSpeed = 3000
                
        };
        playerClient.PlayerState.ServerXPos = defaultDataMessage.startXPos;
        playerClient.PlayerState.ServerYPos = defaultDataMessage.startYPos;
        Console.WriteLine("Sending player default data");
        await MessageHandler.SendMessageAsync(defaultDataMessage, playerClient.StreamWriter);

    } 
}