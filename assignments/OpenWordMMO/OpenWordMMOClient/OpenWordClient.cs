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
        while (true)
        {
            Console.WriteLine("Please enter a word, less than 20 characters. No whitespaces allowed");
            var stringInput = Console.ReadLine();
            if (stringInput == null) return;
            var message = Encoding.ASCII.GetBytes(stringInput);
            client.Send(message, message.Length, serverEndPoint);
            
        }
    }
}