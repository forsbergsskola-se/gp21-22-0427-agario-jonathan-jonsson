using System;
using System.Net.Http;
using System.Numerics;
using System.Threading.Tasks;
using Assets.Scripts.AgarioShared.Network;
using Assets.Scripts.AgarioShared.Network.Messages;

namespace Assets.Scripts.AgarioShared.Model
{
    public class PlayerState
    {
        
        //TODO: these values should be sent from server on game start. also only need getters then I think? Cheat possibilities here otherwise?
        public float CurrentXPos;
        public float CurrentYPos;
        public bool IllegalMovement;
        public string PlayerName;
        public float PlayerSpeed;
        public float ServerXPos;
        public float ServerYPos;
        public float Size;
        public int Score;

        public Vector2 GetPlayerCurrentPosition()
        {
            return new Vector2(CurrentXPos, CurrentYPos);
        }

        
    }
}