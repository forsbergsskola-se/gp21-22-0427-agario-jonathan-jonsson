using System.Net;
using System.Net.Sockets;
using System.Text;

namespace OpenWordMMO;

public class OpenWordServer
{
    private static void Main()
    {
        var serverEndPoint = new IPEndPoint(IPAddress.Loopback, 1313);

        var additiveString = "";

        while (true)
        {
            var server = new UdpClient(serverEndPoint);

            IPEndPoint clientEndPoint = default;
            var response = server.Receive(ref clientEndPoint);
            var responseString = Encoding.ASCII.GetString(response);

            if (responseString.Length > 20 || responseString.Any(char.IsWhiteSpace))
            {
                Console.WriteLine("ERROR: Word is longer than 20 characters or contains whitespaces");
                //Feedback to client here
             
            }
            else
            {
                additiveString += " " + responseString;
                Console.WriteLine($"Packets received from: {clientEndPoint} saying: {additiveString}");

            }
        
            
            server.Close();
        }
    }
}