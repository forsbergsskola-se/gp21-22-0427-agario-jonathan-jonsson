using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void Connect()
    {
        var tcpClient = new TcpClient();
        tcpClient.Connect(IPAddress.Loopback, 1313);
        Debug.Log($"Connected to: {tcpClient.Client.LocalEndPoint}");
        SceneManager.LoadScene("AgarioMain");

    }
}


