using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerClient : MonoBehaviour
{
    private readonly IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Loopback, 1313);

    // public void  ConnectTrigger() => Connect();
    private Stream stream;
    private StreamWriter streamWriter;

    private TcpClient tcpClient;
    private void Connect()
    {
        tcpClient = new TcpClient();
        tcpClient.Connect(serverEndPoint.Address, serverEndPoint.Port);
        Debug.Log($"Connected to: {serverEndPoint.Address}");
         stream = tcpClient.GetStream();
         streamWriter = new StreamWriter(stream);
         streamWriter.AutoFlush = true;
    }

    private void Start()
    {
        Connect();
        PlayerSpawn();
    }

    private void Update()
    {
        //Receive block
        ReceiveOtherPlayerPosition();
        ReceiveUpdatedVisuals();
        
        //Broadcast block
        UpdatePosition();
        UpdatePlayerVisual();
    }

    private void ReceiveUpdatedVisuals()
    {
        throw new NotImplementedException();
    }

    private void ReceiveOtherPlayerPosition()
    {
        throw new NotImplementedException();
    }

    private void PlayerSpawn()
    {
        throw new NotImplementedException();
    }
    private void UpdatePlayerVisual()
    {
        throw new NotImplementedException();
    }

    private void UpdatePosition()
    {
 
        streamWriter.Write($"My position is: {transform.position.ToString()}");

    }


    private void Disconnect()
    {
        tcpClient.Close();
    }
}
