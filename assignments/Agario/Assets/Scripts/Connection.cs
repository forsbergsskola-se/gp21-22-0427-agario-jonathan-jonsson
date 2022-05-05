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
        new Task(ReadMessage).Start();
      
        
        await SendMessage(new LogInMessage()
            {
                messageName = MessagesEnum.LogInMessage,
                playerName = this.playerName
            }
        );
    }

    public async Task SendMessage<T>(T message)
    {
         await streamWriter.WriteLineAsync(JsonUtility.ToJson(message));
         await streamWriter.FlushAsync();
    }

    public void ReadMessage()
    {
        var streamReader = new StreamReader(client.GetStream());

        while (true)
        {
            var inputJson = streamReader.ReadLine();

            var message = JsonUtility.FromJson<Message>(inputJson);

            switch (message.messageName)
            {
                case MessagesEnum.StringMessage:
                    var specificMessage = JsonUtility.FromJson<StringMessage>(inputJson);
                    Debug.Log(specificMessage.stringText);
                    break;
                default:
                    throw new Exception("ERROR: Message class not found when reading data from server!");
            }

        }
    }
}
