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
    private MainClient mainClient => FindObjectOfType<MainClient>();
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Orb"))
        {
            var colOrbId = col.GetComponent<SpawnedOrbData>().orbId;
            CheckOrbPositionValidity(colOrbId, transform.position);
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

        // Debug.Log($"Checking orb {msg.colOrbId} at pos: {msg.XCol},{msg.YCol}");

        await MessageHandler.SendMessageAsync(msg, mainClient.StreamWriter);

    }
 
}
