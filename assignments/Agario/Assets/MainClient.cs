using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class MainClient : MonoBehaviour
{
    [SerializeField]
    private TcpClient client;
    [SerializeField]
    private string playerName;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void Init(TcpClient client, string playerName)
    {
        this.client = client;
        this.playerName = playerName;

        Debug.Log(client.Client.LocalEndPoint);
        Debug.Log(playerName);
    }
    
}
