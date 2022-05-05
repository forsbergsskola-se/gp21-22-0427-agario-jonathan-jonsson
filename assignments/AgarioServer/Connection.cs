using System.Net.Sockets;
using System.Text.Json;

namespace AgarioServer;

public class Connection
{

    public TcpClient client;
    public StreamWriter StreamWriter;
    public string playerName;
    private readonly JsonSerializerOptions options = new ()
    {
        IncludeFields = true
    };
    
    public async Task Init(TcpClient client)
    {
        this.client = client;
        StreamWriter = new StreamWriter(client.GetStream());
        new Task(()=>ReadMessage()).Start();
        
    }

    public async Task SendMessage<T>(T message)
    {
       await StreamWriter.WriteLineAsync(JsonSerializer.Serialize(message, options));
       await StreamWriter.FlushAsync();
    }
    
    
    public async Task ReadMessage()
    {
        var streamReader = new StreamReader(client.GetStream());
 

        while (true)
        {
           var inputJson = streamReader.ReadLine();
            var message = JsonSerializer.Deserialize<Message>(inputJson, options);

            switch (message.messageName)
            {
                case MessagesEnum.LogInMessage:
                    var specificMessage = JsonSerializer.Deserialize<LogInMessage>(inputJson, options);
                    Console.WriteLine($"{specificMessage.playerName} ({client.Client.RemoteEndPoint}) joined the server!");
                    playerName = specificMessage.playerName;
                    break;
                default:
                    throw new Exception("ERROR: Specific message not found on server!");
            }
           
        }
    }
}