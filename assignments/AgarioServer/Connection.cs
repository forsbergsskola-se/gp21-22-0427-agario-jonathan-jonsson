using System.Net.Sockets;
using System.Text.Json;

namespace AgarioServer;

public class Connection
{

    public TcpClient client;
    public StreamWriter streamWriter;
    private static int id;
    public PlayerState playerState;

    public Connection()
    {
        playerState= new PlayerState()
        {
            PlayerServerId = ++id
        };
    }
    public async Task Init(TcpClient client)
    {
        this.client = client;
        streamWriter = new StreamWriter(client.GetStream());
        streamWriter.AutoFlush = true;
        new Task(()=>MessageHandler.ReadMessage(this.client,playerState)).Start();
        
    }
}