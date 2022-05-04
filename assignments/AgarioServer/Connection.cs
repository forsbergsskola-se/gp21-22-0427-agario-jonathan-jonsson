using System.Net.Sockets;
using System.Text.Json;

namespace AgarioServer;

public class Connection
{

    private TcpClient client;
    
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
            //Add stream receiving here
        }
        
        
        
        
        
        
    }
    
    
    
    
    
    
    
}