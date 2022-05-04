using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class RequestServerTime : MonoBehaviour
{

    public Action<string> OnRequestDateAndTime;
    
    IPEndPoint ServerEndPoint = new (IPAddress.Loopback, 1313);
    IPEndPoint ClientEndPoint = new (IPAddress.Loopback, 3210);

    public void SendRequestTrigger() => SendRequest(); 
    
    private async Task SendRequest()
    {
        var TCPClient = new TcpClient(ClientEndPoint);
        
       await TCPClient.ConnectAsync(ServerEndPoint.Address, ServerEndPoint.Port);

        var stream = TCPClient.GetStream();
        var buffer = new byte[100];
        stream.Read(buffer, 0, 100);
        var serverBufferResponse = Encoding.ASCII.GetString(buffer);
        OnRequestDateAndTime?.Invoke(serverBufferResponse);
        TCPClient.Close();
    }
}
