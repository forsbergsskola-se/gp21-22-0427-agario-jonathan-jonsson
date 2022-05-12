using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using Assets.Scripts.AgarioShared.Network;
using Assets.Scripts.AgarioShared.Network.Messages;
using Game;
using Network;
using UnityEngine;

namespace AgarioShared.Network
{
    public class MessageHandler : MonoBehaviour //TODO: not dependent on MB and move to sharedCode
    {

        public Action OnSpawnOrb;
    
        [SerializeField]
        private MainClient mainClient;

        public static async Task SendMessageAsync<T>(T message, StreamWriter streamWriter)
        {
            lock (streamWriter)
            {
                 streamWriter.WriteLineAsync(JsonUtility.ToJson(message));
                 streamWriter.FlushAsync();    
            }
            
        }    
    
        //TODO: Reflection here
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
                        mainClient.playerState.ServerXPos = vector2Message.X;
                        mainClient.playerState.ServerYPos = vector2Message.Y;
                        // Debug.Log($"vector2message: X={vector2Message.X},Y={vector2Message.Y}");
                        break;

                    case MessagesEnum.BoolMessage:
                        var boolMessage = JsonUtility.FromJson<BoolMessage>(inputJson);
                        mainClient.playerState.IllegalMovement = boolMessage.BoolValue;
                        break;
                
                    case MessagesEnum.SpawnOrbMessage:
                        var spawnOrbMessage = JsonUtility.FromJson<SpawnOrbMessage>(inputJson);
                        mainClient.OrbSpawner.X = spawnOrbMessage.X;
                        mainClient.OrbSpawner.Y = spawnOrbMessage.Y;
                    
                        ExecuteOnMainThread.Instance.ExecuteActionOnMainThread(OnSpawnOrb);
                    
                        // Debug.Log($"Spawning orb at: {mainClient.OrbSpawner.X},{mainClient.OrbSpawner.Y}");
                        break;

                    case MessagesEnum.OrbPositionsMessage:
                        break;
                    case MessagesEnum.ValidateOrbPositionMessage:
                        break;
                    default:
                        throw new Exception("ERROR: Message class not found when reading data from server!");
                }

            }
        }
    }
}
