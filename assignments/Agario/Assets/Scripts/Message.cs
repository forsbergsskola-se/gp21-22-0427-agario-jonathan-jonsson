using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message
{
    public string messageName;
}

public class Message<T> : Message
{
    public T value;
}