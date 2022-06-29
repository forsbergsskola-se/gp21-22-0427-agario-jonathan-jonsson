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
            var serverInput = server.Receive(ref clientEndPoint);
            var responseString = Encoding.ASCII.GetString(serverInput).Trim();

            byte[] serverFeedback;

            try
            {
                //Need to throw some kind of exception instead of my hacky way here I think...?
                if (responseString.Length > 20 || responseString.Any(char.IsWhiteSpace))
                {
                    Console.WriteLine("ERROR: Word is longer than 20 characters or contains whitespaces");
                    // serverFeedback = Encoding.ASCII.GetBytes("ERROR: Word is longer than 20 characters or contains whitespaces");
                    throw new Exception("ERROR: Word is longer than 20 characters or contains whitespaces.");

                }
                 
                    additiveString += " " + responseString;
                    Console.WriteLine($"Packets received from: {clientEndPoint} saying: {additiveString}");
                    serverFeedback = Encoding.ASCII.GetBytes(additiveString); 
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
            
            server.Send(serverFeedback, serverFeedback.Length, clientEndPoint);
            server.Close();   
        }
    }
}