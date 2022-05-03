

using System.Net;
using System.Net.Sockets;
using System.Text;

 
public class AgarioServer
{
    private static TcpClient tcpClient;
    static async Task Main()
    {
        var endpoint = new IPEndPoint(IPAddress.Loopback, 1313);

        var tcpListener = new TcpListener(endpoint);
        tcpListener.Start();
        
        
        while (true)
        {
                tcpClient =  await tcpListener.AcceptTcpClientAsync();
                Console.WriteLine(tcpClient.Client.RemoteEndPoint +" has joined the server.");
                
                //RECEIVE BLOCK
                ReceivePosition();
                ReceivePlayerVisual();
                ReceivePlayerScore();

                //BROADCAST BLOCK
                SpawnOrb();
                IncreaseScore();
                UpdatePlayerVisual();


        }

    }

    private static void UpdatePlayerVisual()
    {
        throw new NotImplementedException();
    }

    private static void IncreaseScore()
    {
        throw new NotImplementedException();
    }

    private static void SpawnOrb()
    {
        throw new NotImplementedException();
    }

    private static void ReceivePlayerScore()
    {
        throw new NotImplementedException();
    }

    private static void ReceivePlayerVisual()
    {
        throw new NotImplementedException();
    }

    private static void ReceivePosition()
    {
        throw new NotImplementedException();
    }

    private static void Disconnect()
    {
        throw new NotImplementedException();
        // tcpClient.Dispose();
    }
}

