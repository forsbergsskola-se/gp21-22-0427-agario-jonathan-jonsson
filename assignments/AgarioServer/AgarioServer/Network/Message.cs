using Assets.Scripts.AgarioShared.Network;

namespace AgarioServer.Network;

public class Message
{
    public MessagesEnum MessageName;
}

public class Message<T> : Message
{
    public T Value;
}