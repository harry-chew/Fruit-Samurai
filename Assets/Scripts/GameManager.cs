using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int Score;
    public int FinalScore;

    public float multiplier = 1f;
    public float currentMultiplierTimer;
    public float multiplierTimer;


    public bool endGame;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        endGame = false;
    }

    private void Update()
    {
        HandleScoreMultiplier();

        if (endGame)
        {
            Time.timeScale -= Time.deltaTime / 2;

            if (Time.timeScale <= 0.1f)
            {
                GameOver();
            }
        }
    }

    private void HandleScoreMultiplier()
    {
        currentMultiplierTimer -= Time.deltaTime;
        if (currentMultiplierTimer <= 0.0f)
        {
            multiplier /= 2;
            if (multiplier <= 1.0f)
            {
                multiplier = 1.0f;
            }
            currentMultiplierTimer = multiplierTimer;
        }
    }

    private void Start()
    {
        GameEvents.FruitEvent += OnFruitEvent;
        GameEvents.GameEvent += OnGameEvent;
        GameEvents.BombEvent += OnBombEvent;
        SceneManager.sceneLoaded += OnSceneLoaded;
        currentMultiplierTimer = multiplierTimer;
    }

    private void OnBombEvent(object sender, BombEventArgs e)
    {
        if (e.EventType == BombEventType.Cut)
        {
            endGame = true;
            FinalScore = Score;
        }
    }

    private void GameOver()
    {
        GameEvents.FireGameEndEvent();
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == "GameOver")
        {
            GameEvents.FireGameOverEvent(FinalScore);
        }
        if (arg0.name == "Menu")
        {
            Score = 0;
            FinalScore = Score;
        }
    }

    private void OnDisable()
    {
        GameEvents.FruitEvent -= OnFruitEvent;
        GameEvents.GameEvent -= OnGameEvent;
    }

    private void OnGameEvent(object sender, GameEventArgs e)
    {
        if (e.EventType == GameEventType.End)
        {
            SceneManager.LoadScene("GameOver");
            GameEvents.GameEvent -= OnGameEvent;
        }
    }

    private void OnFruitEvent(object sender, FruitEventArgs e)
    {
        if (e.EventType == FruitEventType.Cut)
        {
            Score += (e.Fruit.scoreValue * (int)Math.Floor(multiplier));
            multiplier *= 2;
        }
    }
}