using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgarioShared.Network;
using Assets.Scripts.AgarioShared.Network;
using Assets.Scripts.AgarioShared.Network.Messages;
using Network;
using UnityEngine;

public class OrbCollector : MonoBehaviour
{
    [SerializeField] private MainClient mainClient;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Orb"))
        {

            //Send position to check to server
            var colOrbId = col.GetComponent<SpawnedOrbData>().orbId;
            CheckOrbPositionValidity(colOrbId, transform.position);
            //Server sees if it is in list
            //If so, returns msg with OK and removes it from the current list.
   
        }
    }

    public async Task CheckOrbPositionValidity(int colOrbId, Vector2 playerPos)
    {
      
        var msg = new VerifyValidOrbPickupMessage()
        {
            MessageName = MessagesEnum.VerifyValidOrbPickupMessage,
            colOrbId = colOrbId,
            XCol = playerPos.x,
            YCol = playerPos.y
        };

        Debug.Log($"Checking orb {msg.colOrbId} at pos: {msg.XCol},{msg.YCol}");

        await MessageHandler.SendMessageAsync(msg, mainClient.StreamWriter);

    }
 
}
