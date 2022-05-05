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

    public void Init(TcpClient client, string playerName)
    {
        this.client = client;
        this.playerName = playerName;
        streamWriter = new StreamWriter(client.GetStream());
        new Thread(ReadMessage).Start();
       SendMessage(new LogInMessage()
            {
                messageName = "LogInMessage",
                playerName = this.playerName
            }
        );
    }

    public void SendMessage<T>(T message)
    {
         streamWriter.WriteLineAsync(JsonUtility.ToJson(message));
         streamWriter.FlushAsync();
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
                case "TestMessage":
                    var specificMessage = JsonUtility.FromJson<TestMessage>(inputJson);
                    Debug.Log(specificMessage.testString);
                    break;
                default:
                    throw new Exception("ERROR: Message class not found when reading data from server!");
            }

        }
    }
}
