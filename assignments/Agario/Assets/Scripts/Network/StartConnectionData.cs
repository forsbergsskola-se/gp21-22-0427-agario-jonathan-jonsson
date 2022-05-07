using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartConnectionData : MonoBehaviour
{
    public TMP_InputField nameInput;
    public TcpClient TcpClient = new TcpClient();
    public string playerName;
    public void ConnectOnClick() => Connect();
    
    public async Task Connect()
    {
 
        await TcpClient.ConnectAsync(IPAddress.Loopback, 1313);
        playerName = nameInput.text;
        SceneManager.LoadSceneAsync("AgarioMain");

    }

    private void Start()
    {
        DontDestroyOnLoad(this);
    }
}
