using System.Net;
using System.Net.Sockets;
using System.Text;

public class TCPServer
{
    async static Task Main()
    {
        var endPoint = new IPEndPoint(IPAddress.Loopback, 1313);
        var TCPListener = new TcpListener(endPoint);
        
        TCPListener.Start();

        while (true)
        {
            var TCPClient = await TCPListener.AcceptTcpClientAsync();
            var responseBuffer = Encoding.ASCII.GetBytes(DateTime.Now.ToString());
            TCPClient.GetStream().Write(responseBuffer, 0, responseBuffer.Length);

            TCPClient.GetStream().Close();
            TCPClient.Close();
        }      
    }
}