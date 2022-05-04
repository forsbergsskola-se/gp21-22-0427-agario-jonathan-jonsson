using System.Net;
using System.Net.Sockets;
using System.Text;
using TMPro;
using UnityEngine;

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
            var stringInput = inputField.text;
            var message = Encoding.ASCII.GetBytes(stringInput);
            client.Send(message, message.Length, serverEndpoint);
            ReceiveServerResponse();
            client.Close();
    }

    private void ReceiveServerResponse()
    {
        var response = client.Receive(ref serverEndpoint);
        
        //Issue here: the client cant really discern the message retrieved from server as an error-message. So updating UI even when the return response is a string of warning instead of input.
        Debug.Log("Server response: "+Encoding.ASCII.GetString(response));
    }
}
