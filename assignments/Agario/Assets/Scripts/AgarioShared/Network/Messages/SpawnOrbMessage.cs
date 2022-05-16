using System.Numerics;

namespace Assets.Scripts.AgarioShared.Network.Messages
{
    public class SpawnOrbMessage : Message
    {
        public int orbId;
        public float X;
        public float Y;
    }
}