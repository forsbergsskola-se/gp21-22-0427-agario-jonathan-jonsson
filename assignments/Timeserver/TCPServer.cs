using System.Net;
using System.Net.Sockets;
using System.Text;

var endPoint = new IPEndPoint(IPAddress.Loopback, 1441);

var TCPListener = new TcpListener(endPoint);
TCPListener.Start();

while (true)
{
    var TCPClient = TCPListener.AcceptTcpClient();
    byte[] buffer = new byte[100];
    TCPClient.GetStream().Read(buffer, 0, 100);
    Console.WriteLine($"Client Input: " +Encoding.ASCII.GetString(buffer));
    var responseBuffer = Encoding.ASCII.GetBytes(DateTime.Now.ToString());
    Console.WriteLine();
    TCPClient.GetStream().Write(responseBuffer,0,responseBuffer.Length);
    TCPClient.Close();
}