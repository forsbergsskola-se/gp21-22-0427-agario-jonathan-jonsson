using System;
using System.Collections;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Connection
{
    [SerializeField]
    private TcpClient client;
    [SerializeField] public string playerName;

    public StreamWriter streamWriter;

    

    public async Task Init(TcpClient client, string playerName)
    {
        this.client = client;
        this.playerName = playerName;
        streamWriter = new StreamWriter(client.GetStream());
        new Task(()=>MessageHandler.ReadMessage(this.client)).Start();
    }

   



   
}
