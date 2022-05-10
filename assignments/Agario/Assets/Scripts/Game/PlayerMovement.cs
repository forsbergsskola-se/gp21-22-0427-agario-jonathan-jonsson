using System;
using Assets.Scripts.AgarioShared.Model;
using Network;
using UnityEngine;

namespace Game
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb2d;
         private PlayerState playerState => FindObjectOfType<MainClient>().playerState;

        private float horizontal;
        private float vertical;

        private void Start()
        {
            Debug.LogError("To get console in build");
        }

        private void Update()
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
            playerState.CurrentXPos = transform.position.x;
            playerState.CurrentYPos = transform.position.y;
        }

        private void LateUpdate()
        {
            if (playerState.IllegalMovement) // if player try to move outside map --> illegal state sent by server --> position corrected from server here
            {
                transform.position = new Vector2(playerState.ServerXPos, playerState.ServerYPos);
                Debug.Log($"Trying to exit board. Server corrected position to: {transform.position} ({playerState.ServerXPos},{playerState.ServerYPos})");
                return;
            }
                
            var dir = new Vector2(horizontal, vertical).normalized;
            rb2d.velocity = dir * (playerState.PlayerSpeed * Time.deltaTime);
            //speed should not be playerState
        }
    }
}
