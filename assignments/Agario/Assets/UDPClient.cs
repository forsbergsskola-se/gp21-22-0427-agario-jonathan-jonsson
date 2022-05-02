using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UDPClient : MonoBehaviour
{
    IPEndPoint serverEndpoint = new(IPAddress.Loopback, 1313);
    IPEndPoint clientEndpoint = new(IPAddress.Loopback, 1314);

    [SerializeField]
    private TMP_InputField inputField;

    private UdpClient client;
    
    
    public void SendChatMsg()
    {
            Debug.Log("Please enter a word, less than 20 characters. No whitespaces allowed");
            client = new UdpClient(clientEndpoint);
            // var stringInput = Console.ReadLine();
            var stringInput = inputField.text;
            if (string.IsNullOrEmpty(stringInput)) return;
            var message = Encoding.ASCII.GetBytes(stringInput);
            client.Send(message, message.Length, serverEndpoint);
            ReceiveServerResponse();
            client.Close();
    }

    private void ReceiveServerResponse()
    {
        byte[] response = client.Receive(ref serverEndpoint);
        Debug.Log("Server response: "+Encoding.ASCII.GetString(response));
    }
}
