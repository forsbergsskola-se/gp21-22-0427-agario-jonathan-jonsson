using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class RequestServerTime : MonoBehaviour
{

    // public void SendRequest() => Debug.Log("Button pressed - update date and time");

    private void SendRequest()
    {

        var ServerEndPoint = new IPEndPoint(IPAddress.Loopback, 1111);
        var ClientEndPoint = new IPEndPoint(IPAddress.Loopback, 1112);

        var TCPClient = new TcpClient(ClientEndPoint);
        
        TCPClient.Connect(ServerEndPoint);

        var stream = TCPClient.GetStream();
        byte[] buffer = new byte[100];
        stream.Read(buffer, 0, 100);
        Debug.Log("Server says: " +Encoding.ASCII.GetString(buffer));
        
        TCPClient.Close();

    }




}
