using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using UnityEngine;

public class MainClient : MonoBehaviour
{
    [SerializeField]
    private TcpClient client;
    [SerializeField]
    private string playerName;

    private StreamWriter streamWriter;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void Init(TcpClient client, string playerName)
    {
        this.client = client;
        this.playerName = playerName;
        streamWriter = new StreamWriter(client.GetStream());
        
    }

    public void SendMessage<T>(T message)
    {
        streamWriter.WriteLine(JsonUtility.ToJson(message));
        streamWriter.Flush();
    }
    
}
