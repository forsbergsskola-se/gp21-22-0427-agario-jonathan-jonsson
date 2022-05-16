namespace Assets.Scripts.AgarioShared.Network.Messages
{
    public class OrbValidationResponseMessage : Message
    {
        public int orbId;
        public bool orbValid;
    }
}