using System.Collections;
using System.Collections.Generic;
using Messages;
using UnityEngine;

public class PositionTransmitter : MonoBehaviour
{
    
[SerializeField] private PlayerClient playerClient;
    
    private void Start()
    {

        //TODO: IF current pos != stored pos on server:
        StartCoroutine(UpdatePosToServer());
    }
    
    IEnumerator UpdatePosToServer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            var msg = new Vector2Message()
            {
                messageName = MessagesEnum.Vector2Message,
                x = transform.position.x,
                y = transform.position.y
    
    
            };
            
            playerClient.MessageHandler.SendMessageAsync(msg, playerClient.streamWriter);
    
    
        }
    }
}
