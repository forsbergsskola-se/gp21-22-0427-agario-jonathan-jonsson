namespace Assets.Scripts.AgarioShared.Network.Messages
{
    public class OrbPositionValidationResponseMessage : Message
    {
        public bool orbPositionValid;
        public float X;
        public float Y;
    }
}