using System.Net.Sockets;
using System.Text.Json;

namespace AgarioServer;

public class Connection
{
    public playerClient playerClient;
    public StreamWriter streamWriter;
    private static int id;

    public Connection()
    {
        playerClient= new playerClient()
        {
            playerServerId = ++id,
            playerState = new PlayerState()
        };
    }
    public async Task Init(TcpClient tcpClient)
    {
        playerClient.playerTcpClient = tcpClient;
        streamWriter = new StreamWriter(tcpClient.GetStream());
        streamWriter.AutoFlush = true;
        new Task(()=>MessageHandler.ReadMessage(playerClient)).Start();
        
    }
}