using System.Net.Sockets;
using System.Text.Json;

namespace AgarioServer;

public class Connection
{

    public TcpClient client;
    
    public Connection(TcpClient client)
    {
        this.client = client;
        new Thread(ReadMessage).Start();
    }

    public void ReadMessage()
    {
        var streamReader = new StreamReader(client.GetStream());
        var options = new JsonSerializerOptions()
        {
            IncludeFields = true
        };

        while (true)
        {
            var inputJson = streamReader.ReadLine();
            var message = JsonSerializer.Deserialize<Message>(inputJson, options);
            Console.WriteLine($"Message name: {message.messageName}");
            if (message.messageName == "LogInMessage")
            {
                var value = JsonSerializer.Deserialize<Message<LogInMessage>>(inputJson, options);
                Console.WriteLine(value.value.playerName +" connected on " +client.Client.RemoteEndPoint);
            }
        }
    }
}