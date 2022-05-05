using System.Net.Sockets;
using System.Text.Json;

namespace AgarioServer;

public class Connection
{

    public TcpClient client;
    public StreamWriter StreamWriter;
    private static int id;
    public PlayerState playerState;

    public Connection()
    {
        playerState= new PlayerState()
        {
            PlayerServerId = ++id
        };
    }
    
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
        
    public async Task SendMessageAsync<T>(T message)
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

                    playerState.playerName = specificMessage.playerName;
                    new Task(()=> SendWelcomeResponse(playerState)).Start(); //TODO: Is it really smart having this here?
                    
                    break;
                default:
                    throw new Exception("ERROR: Specific message not found on server!");
            }
           
        }
    }
    
    public  async Task SendWelcomeResponse(PlayerState playerConnection)
    {
      await SendMessageAsync(new StringMessage()
        {
            messageName = MessagesEnum.StringMessage,
            stringText = $"Welcome to the server, {playerState.playerName}. You have been assigned ID: {playerState.PlayerServerId}"
        });

    }
}