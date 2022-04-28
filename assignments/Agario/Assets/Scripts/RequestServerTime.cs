using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class RequestServerTime : MonoBehaviour
{

    public Action<string> OnRequestDateAndTime;
    
    IPEndPoint ServerEndPoint = new (IPAddress.Loopback, 1111);
    IPEndPoint ClientEndPoint = new (IPAddress.Loopback, 1112);
    private void SendRequest()
    {
        var TCPClient = new TcpClient(ClientEndPoint);
        
        TCPClient.Connect(ServerEndPoint);

        var stream = TCPClient.GetStream();
        byte[] buffer = new byte[100];
        stream.Read(buffer, 0, 100);
        var serverBufferResponse = Encoding.ASCII.GetString(buffer);
        // Debug.Log("Server says: " +Encoding.ASCII.GetString(buffer));
        OnRequestDateAndTime?.Invoke(serverBufferResponse);
        TCPClient.Close();
    }
}
