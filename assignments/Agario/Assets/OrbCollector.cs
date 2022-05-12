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
            CheckOrbPositionValidity(col.transform.position);
            //Server sees if it is in list
            //If so, returns msg with OK and removes it from the current list.
   
        }
    }

    public async Task CheckOrbPositionValidity(Vector2 orbPos)
    {
        var orbPositionMsg = new ValidateOrbPositionMessage()
        {
            MessageName = MessagesEnum.ValidateOrbPositionMessage,
            X = orbPos.x,
            Y = orbPos.y
        };
        await MessageHandler.SendMessageAsync(orbPositionMsg, mainClient.StreamWriter);
    }
 
}
