using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerClient : MonoBehaviour
{
    private readonly IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Loopback, 1313);

    public void  ConnectTrigger() => Connect();
 

    private  void Connect()
    {
        var tcpClient = new TcpClient();
        tcpClient.Connect(serverEndPoint.Address, serverEndPoint.Port);
        var stream = tcpClient.GetStream();
        var streamReader = new StreamReader(stream);
        var streamWriter = new StreamWriter(stream);
        streamWriter.AutoFlush = true;
    }
}
