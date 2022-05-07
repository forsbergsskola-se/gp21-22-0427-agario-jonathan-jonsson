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

        var dir = new Vector2(horizontal, vertical).normalized;
        rb2d.velocity = dir * (playerState.speed * Time.deltaTime);





    }
}
