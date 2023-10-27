using System;

public enum FruitEventType
{
    Cut,
    Drop
}

public class FruitEventArgs : EventArgs
{
    public FruitEventType EventType;
    public Fruit Fruit;
    public FruitEventArgs(FruitEventType eventType, Fruit fruit)
    { 
        EventType = eventType; 
        Fruit = fruit;
    }
}