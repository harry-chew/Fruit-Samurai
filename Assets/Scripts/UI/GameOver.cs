using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public Button replay;

    public void Awake()
    {
        GameEvents.GameEvent += OnGameEvent;
    }

    private void Start()
    {

        replay.GetComponent<CanvasGroup>().alpha = 0.0f;
        replay.onClick.AddListener(OnReplayButtonClick);
    }

    private void OnReplayButtonClick()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Menu");
    }

    private void OnGameEvent(object sender, GameEventArgs e)
    {
        if (e.EventType == GameEventType.GameOver)
        {
            Time.timeScale = 1.0f;
            scoreText.text = e.Score.ToString();
            DOTween.To(() => replay.GetComponent<CanvasGroup>().alpha, x => replay.GetComponent<CanvasGroup>().alpha = x, 1f, 1f);
        }
    }
}
