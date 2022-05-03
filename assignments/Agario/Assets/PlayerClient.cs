using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerClient : MonoBehaviour
{
    [SerializeField]
    public int port = 1314;
    private IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Loopback, 1313);

    public void  ConnectTrigger() => Connect();
    private TcpClient tcpClient;

    private async Task Connect()
    {
        IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Loopback, port);
        var tcpClient = new TcpClient(clientEndPoint);
        await tcpClient.ConnectAsync(serverEndPoint.Address, serverEndPoint.Port);
        string portString = port.ToString();
        Debug.Log(portString);
       await tcpClient.GetStream().WriteAsync(Encoding.ASCII.GetBytes(portString));
    }

    private async Task Disconncet()
    {
        tcpClient.Close();
    }

    
}
