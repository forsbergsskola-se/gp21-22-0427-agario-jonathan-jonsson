namespace Assets.Scripts.AgarioShared.Network.Messages
{
    public class DefaultPlayerStateDataMessage : Message
    {
        public float PlayerSpeed;
        public float Size;
        public int Score;
        public float startXPos;
        public float startYPos;
    }
}