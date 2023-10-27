using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public void Awake()
    {
        GameEvents.GameEvent += OnGameEvent;
    }

    private void OnGameEvent(object sender, GameEventArgs e)
    {
        if (e.EventType == GameEventType.GameOver)
        {
            scoreText.text = e.Score.ToString();
        }
    }
}
