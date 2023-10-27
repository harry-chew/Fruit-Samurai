using System;

public enum GameEventType
{
    Start,
    End,
    GameOver
}

public class GameEventArgs : EventArgs
{
    public GameEventType EventType;
    public int Score;

    public GameEventArgs(GameEventType eventType, int score = 0)
    {
        EventType = eventType;
        Score = score;
    }
}