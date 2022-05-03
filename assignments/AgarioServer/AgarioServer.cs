

using System.Net;
using System.Net.Sockets;
using System.Text;

public class AgarioServer
{
    private static TcpClient tcpClient;
    static void Main()
    {
        var endpoint = new IPEndPoint(IPAddress.Loopback, 1313);

        var tcpListener = new TcpListener(endpoint);
        tcpListener.Start();
        
        
        while (true)
        {
            new Thread(() =>
            {
                tcpClient =  tcpListener.AcceptTcpClient();
                var stream = tcpClient.GetStream();
                var streamWriter = new StreamWriter(stream);
                var streamReader = new StreamReader(stream);
                streamWriter.AutoFlush = true;
                Console.WriteLine(tcpClient.Client.RemoteEndPoint +" has joined the server.");
                streamWriter.WriteLine($"Welcome {tcpClient.Client.RemoteEndPoint}!");
                tcpClient.Dispose();
            }).Start();
             

        }

    }
 
 
}

