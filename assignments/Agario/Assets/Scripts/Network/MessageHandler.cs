using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using Messages;
using Unity.VisualScripting;
using UnityEngine;

public class MessageHandler : MonoBehaviour
{
    [SerializeField]
    private PlayerClient playerClient;
    
    public async Task SendMessageAsync<T>(T message, StreamWriter streamWriter)
    {
        await streamWriter.WriteLineAsync(JsonUtility.ToJson(message));
        await streamWriter.FlushAsync();
    }    
    
    public async Task ReadMessage(TcpClient client)
    {
        var streamReader = new StreamReader(client.GetStream());

        while (true)
        {
            var inputJson = await streamReader.ReadLineAsync();
            var message = JsonUtility.FromJson<Message>(inputJson);
            
            switch (message.messageName)
            {
                case MessagesEnum.StringMessage:
                    var stringMessage = JsonUtility.FromJson<StringMessage>(inputJson);
                    Debug.Log(stringMessage.stringText);
                    break;
                case MessagesEnum.ServerIdAssignmentMessage:
                    var serverIDAssignmentMessage = JsonUtility.FromJson<ServerIDAssignmentMessage>(inputJson);
                    playerClient.ServerID = serverIDAssignmentMessage.ID;
                    break;
                case MessagesEnum.LogInMessage:
                    break;

                case MessagesEnum.Vector2Message:
                    var Vector2Message = JsonUtility.FromJson<Vector2Message>(inputJson);
                    playerClient.playerState.XPos = Vector2Message.x;
                    playerClient.playerState.YPos = Vector2Message.y;
                    // Debug.Log($"vector2message: X={Vector2Message.x},Y={Vector2Message.y}");
                    break;

                case MessagesEnum.BoolMessage:
                    var boolMessage = JsonUtility.FromJson<BoolMessage>(inputJson);
                    playerClient.playerState.IllegalMovement = boolMessage.boolValue;
                    break;
                default:
                    throw new Exception("ERROR: Message class not found when reading data from server!");
            }

        }
    }
}
