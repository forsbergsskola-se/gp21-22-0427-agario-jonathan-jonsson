using System;
using System.Collections;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Connection : MonoBehaviour
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

    public async Task Init(TcpClient client, string playerName)
    {
        this.client = client;
        this.playerName = playerName;
        streamWriter = new StreamWriter(client.GetStream());
        new Thread(()=>ReadMessage()).Start();
        SendClientLogInMessage();
    }

    private void SendClientLogInMessage()
    {
        new Task(() => SendMessageAsync(new LogInMessage()
            {
                messageName = MessagesEnum.LogInMessage,
                playerName = this.playerName
            }
        )).Start();
    }

    public async Task SendMessageAsync<T>(T message)
    {
         await streamWriter.WriteLineAsync(JsonUtility.ToJson(message));
         await streamWriter.FlushAsync();
    }

    public async Task ReadMessage()
    {
        var streamReader = new StreamReader(client.GetStream());

        while (true)
        {
            var inputJson = await streamReader.ReadLineAsync();
            var message = JsonUtility.FromJson<Message>(inputJson);
            
            switch (message.messageName)
            {
                case MessagesEnum.StringMessage:
                    var specificMessage = JsonUtility.FromJson<StringMessage>(inputJson);
                    Debug.Log(specificMessage.stringText);
                    break;
                case MessagesEnum.ServerIDAssignmentMessage:
                    var serverIDAssignmentMessage = JsonUtility.FromJson<ServerIDAssignmentMessage>(inputJson);
                    Debug.Log(serverIDAssignmentMessage.ID);
                    break;
                default:
                    throw new Exception("ERROR: Message class not found when reading data from server!");
            }

        }
    }
}
