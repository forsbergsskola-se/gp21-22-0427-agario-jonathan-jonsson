using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class MessageHandler : MonoBehaviour
{
    [SerializeField]
    private MainClient mainClient;

    public static async Task SendMessageAsync<T>(T message, StreamWriter streamWriter)
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
            
            switch (message.MessageName)
            {
                case MessagesEnum.StringMessage:
                    var stringMessage = JsonUtility.FromJson<StringMessage>(inputJson);
                    Debug.Log(stringMessage.StringText);
                    break;
                case MessagesEnum.ServerIdAssignmentMessage:
                    var serverIDAssignmentMessage = JsonUtility.FromJson<ServerIDAssignmentMessage>(inputJson);
                    mainClient.ServerID = serverIDAssignmentMessage.Id;
                    break;
                case MessagesEnum.LogInMessage:
                    break;

                case MessagesEnum.Vector2Message:
                    var vector2Message = JsonUtility.FromJson<Vector2Message>(inputJson);
                    mainClient.playerState.XPos = vector2Message.X;
                    mainClient.playerState.YPos = vector2Message.Y;
                    // Debug.Log($"vector2message: X={Vector2Message.x},Y={Vector2Message.y}");
                    break;

                case MessagesEnum.BoolMessage:
                    var boolMessage = JsonUtility.FromJson<BoolMessage>(inputJson);
                    mainClient.playerState.IllegalMovement = boolMessage.BoolValue;
                    break;
                
                case MessagesEnum.SpawnOrbMessage:
                    var spawnOrbMessage = JsonUtility.FromJson<SpawnOrbMessage>(inputJson);
                    mainClient.OrbSpawner.X = spawnOrbMessage.X;
                    mainClient.OrbSpawner.Y = spawnOrbMessage.Y;
                    // mainClient.OrbSpawner.SpawnOrb(); //TODO: alot of direct references through main client................ maybe do differently...?
                    Debug.Log($"Spawning orb at: { mainClient.OrbSpawner.X},{mainClient.OrbSpawner.Y}");
                    break;
                
                default:
                    throw new Exception("ERROR: Message class not found when reading data from server!");
            }

        }
    }
}
