using System.Net.Sockets;
using System.Text.Json;

namespace AgarioServer;

public class Connection
{

    public TcpClient client;
    public StreamWriter StreamWriter;
    
    private readonly JsonSerializerOptions options = new ()
    {
        IncludeFields = true
    };
    
    public Connection(TcpClient client)
    {
        this.client = client;
        
        StreamWriter = new StreamWriter(client.GetStream());
        new Thread(ReadMessage).Start();
    }

    public void SendMessage<T>(T message)
    {
        StreamWriter.WriteLine(JsonSerializer.Serialize(message, options));
        StreamWriter.Flush();
    }
    
    
    
    public void ReadMessage()
    {
        var streamReader = new StreamReader(client.GetStream());
       

        while (true)
        {
           var inputJson = streamReader.ReadLine();
            var message = JsonSerializer.Deserialize<Message>(inputJson, options);
            Console.WriteLine($"Message name: {message.messageName}");
            
            if (message.messageName == "LogInMessage")
            {
                var specificMessage = JsonSerializer.Deserialize<LogInMessage>(inputJson, options);
                Console.WriteLine($"{specificMessage.playerName} joined the server ({client.Client.RemoteEndPoint})!");
            }
        }
    }
}