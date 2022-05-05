using System.Collections;
using System.IO;
using System.Net.Sockets;
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
        Debug.Log(this.playerName);

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
 
}
