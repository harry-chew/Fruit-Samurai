using System;

public enum BombEventType
{
    Cut
}

public class BombEventArgs : EventArgs
{
    public BombEventType EventType;
    public Bomb Bomb;

    public BombEventArgs(BombEventType eventType, Bomb bomb)
    {
        EventType = eventType;
        Bomb = bomb;
    }
}