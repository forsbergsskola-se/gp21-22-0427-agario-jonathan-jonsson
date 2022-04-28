using System.Net;
using System.Net.Sockets;
using System.Text;

var endPoint = new IPEndPoint(IPAddress.Loopback, 1111);

var TCPListener = new TcpListener(endPoint);
TCPListener.Start();

while (true)
{
    // TCPListener.Start();
    //Await for Client here somewhere and make async (probably?)
    
    var TCPClient = TCPListener.AcceptTcpClient();
    var responseBuffer = Encoding.ASCII.GetBytes(DateTime.Now.ToString());
    TCPClient.GetStream().Write(responseBuffer, 0, responseBuffer.Length);

    TCPClient.GetStream().Close();
    TCPClient.Close();
    
    // TCPListener.Stop();
}