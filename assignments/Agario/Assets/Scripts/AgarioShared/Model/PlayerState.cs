using System.Numerics;

namespace Assets.Scripts.AgarioShared.Model
{
    public class PlayerState
    {
        
        //TODO: these values should be sent from server on game start. also only need getters then I think? Cheat possibilities here otherwise?
        public float CurrentXPos;
        public float CurrentYPos;
        public bool IllegalMovement;
        public string PlayerName;
        public float PlayerSpeed = 3000;
        public float ServerXPos;
        public float ServerYPos;
        public float Size = 1;

        public Vector2 GetPlayerCurrentPosition()
        {
            return new Vector2(CurrentXPos, CurrentYPos);
        }
    }
}