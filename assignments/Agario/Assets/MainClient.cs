using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MainClient : MonoBehaviour
{
    private readonly IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Loopback, 1313);

    // public void  ConnectTrigger() => Connect();
 

    private TcpClient tcpClient;
    private StreamWriter streamWriter;
    private void Connect()
    {
        tcpClient = new TcpClient();
        tcpClient.Connect(serverEndPoint.Address, serverEndPoint.Port);
        Debug.Log($"Connected to: {serverEndPoint.Address}");
         
    }

    
}
