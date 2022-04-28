using System.Net;
using System.Net.Sockets;
using System.Text;

namespace OpenWordMMOClient;

public class OpenWordClient
{
    static void Main()
    {
        var serverEndPoint = new IPEndPoint(IPAddress.Loopback, 1313);
        var clientEndPoint = new IPEndPoint(IPAddress.Loopback, 1314);

        var client = new UdpClient(clientEndPoint);
        var message = Encoding.ASCII.GetBytes("Hello server");

        client.Send(message, message.Length, serverEndPoint);
    }
}