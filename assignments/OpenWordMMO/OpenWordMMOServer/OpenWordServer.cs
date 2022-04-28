using System.Net;
using System.Net.Sockets;
using System.Text;

namespace OpenWordMMO;

public class OpenWordServer
{
    static void Main()
    {
        var serverEndPoint = new IPEndPoint(IPAddress.Loopback, 1313);
        var server = new UdpClient(serverEndPoint);

        while (true)
        {
            IPEndPoint clientEndPoint = default;
            var response = server.Receive(ref clientEndPoint);
            Console.WriteLine($"Packets recived from: {clientEndPoint} saying: {Encoding.ASCII.GetString(response)}");
        }
    }
}