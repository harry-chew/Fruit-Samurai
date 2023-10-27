using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static EventHandler<GameEventArgs> GameEvent;
    public static EventHandler<FruitEventArgs> FruitEvent;
    public static EventHandler<BombEventArgs> BombEvent;

    public static void FireGameOverEvent(int score)
    {
        GameEventArgs args = new GameEventArgs(GameEventType.GameOver, score);
        GameEvent?.Invoke(null, args);
    }

    public static void FireGameEndEvent()
    {
        GameEventArgs args = new GameEventArgs(GameEventType.End);
        GameEvent?.Invoke(null, args);
    }

    public static void FireFruitCutEvent(Fruit fruit)
    {
        FruitEventArgs args = new FruitEventArgs(FruitEventType.Cut, fruit);
        FruitEvent?.Invoke(null, args);
    }

    public static void FireFruitDropEvent(Fruit fruit)
    {
        FruitEventArgs args = new FruitEventArgs(FruitEventType.Drop, fruit);
        FruitEvent?.Invoke(null, args);
    }

    public static void FireBombCutEvent(Bomb bomb)
    {
        BombEventArgs args = new BombEventArgs(BombEventType.Cut, bomb);
        BombEvent?.Invoke(null, args);
    }
}