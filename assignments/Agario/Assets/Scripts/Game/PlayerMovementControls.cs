using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementControls : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private PlayerState playerState;

    private float horizontal;
    private float vertical;
    private void Update()
    {
         horizontal = Input.GetAxisRaw("Horizontal");
         vertical = Input.GetAxisRaw("Vertical");
    }

    void LateUpdate()
    {

        if (playerState.IllegalMovement)
        {
            transform.position = new Vector2(playerState.XPos, playerState.YPos);
            Debug.Log($"Trying to exit board. Server corrected position to: {transform.position} ({playerState.XPos},{playerState.YPos})");
            return;
        }
        var dir = new Vector2(horizontal, vertical).normalized;
        rb2d.velocity = dir * (playerState.playerSpeed * Time.deltaTime);





    }
}