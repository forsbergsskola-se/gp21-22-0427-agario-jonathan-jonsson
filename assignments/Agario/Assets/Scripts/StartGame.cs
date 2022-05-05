using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    [SerializeField]
    private Connection connection;
    [SerializeField]
    private TMP_InputField nameField;

    public void ConnectOnClick() => Connect();
    
    public async Task Connect()
    {
        var tcpClient = new TcpClient();
        await tcpClient.ConnectAsync(IPAddress.Loopback, 1313);
        var playerName = nameField.text;
        
        await connection.Init(tcpClient, playerName);
        SceneManager.LoadSceneAsync("AgarioMain");

    }
    
    
}


