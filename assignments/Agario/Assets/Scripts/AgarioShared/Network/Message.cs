namespace Assets.Scripts.AgarioShared.Network
{
    public class Message
    {
        public MessagesEnum MessageName;
    }

    public class Message<T> : Message
    {
        public T Value;
    }
}