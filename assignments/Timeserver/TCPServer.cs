using System.Net;
using System.Net.Sockets;
using System.Text;

var endPoint = new IPEndPoint(IPAddress.Loopback, 1111);

var TCPListener = new TcpListener(endPoint);
TCPListener.Start();

while (true)
{
    var TCPClient = TCPListener.AcceptTcpClient();
    // var buffer = new byte[100];
    // var responseBuffer = Encoding.ASCII.GetBytes("Type anything to get Date and time!");
    // TCPClient.GetStream().Write(responseBuffer, 0, responseBuffer.Length);
    // TCPClient.GetStream().Read(buffer, 0, 100);
    // Console.WriteLine("Client Input: " + Encoding.ASCII.GetString(buffer));
    
    var responseBuffer = Encoding.ASCII.GetBytes(DateTime.Now.ToString());
    TCPClient.GetStream().Write(responseBuffer, 0, responseBuffer.Length);
    
    TCPClient.GetStream().Close();
    TCPClient.Close();
}