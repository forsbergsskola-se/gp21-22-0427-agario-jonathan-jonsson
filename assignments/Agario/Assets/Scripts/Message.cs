
public class Message
{
    public MessagesEnum messageName;
}

public class Message<T> : Message
{
    public T value;
}