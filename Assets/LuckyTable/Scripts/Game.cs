using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Game : MonoBehaviour
{
    public Launcher m_defaultLauncher;

    public int m_initialBallCount = 3;
    public GameObject m_ballProto;
    public GameObject m_gameOverEffect;
    public float m_gameOverDuration = 3.0f;

    public int BallsRemaining 
    { 
        get { return m_ballsRemaining; } 
        private set { m_ballsRemaining = value; OnBallsRemainingUpdate?.Invoke(m_ballsRemaining); }
    }
    public int BallsInPlay { get; set; }

    public bool LogGameState = false;

    public event Action OnResetSafeties;
    public event Action OnResetDoors;
    public event Action OnResetGame;
    public UnityEvent<int> OnBallsRemainingUpdate;

    public static Game Instance;

    public Game()
    {
        Debug.Assert(Instance == null);
        Instance = this;
    }

    void Start()
    {
        ResetGame();
    }

    void Update()
    {
        if (m_gameOverTimer > 0.0f)
        {
            m_gameOverTimer -= Time.deltaTime;
            if (m_gameOverTimer <= 0.0f)
            {
                m_gameOverTimer = -1.0f;
                m_gameOverEffect?.SetActive(false);
                ResetGame();
            }
        }
    }

    void OnDestroy()
    {
        Debug.Assert(Instance == this);
        Instance = null;
    }

    public void OnBallLeftPlay(Ball i_ball)
    {
        BallsInPlay--;

        if (BallsInPlay <= 0 && BallsRemaining > 0)
        {
            OnResetSafeties?.Invoke();
            SpawnBall(i_ball);
        }
        else
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        Log("Game Over");
        m_gameOverEffect?.SetActive(true);
        m_gameOverTimer = m_gameOverDuration;
    }

    public void ExtraBall(int i_num = 1)
    {
        Log("Awarded {0} extra balls", i_num);
        BallsRemaining += i_num;
    }

    public void SpawnBall(Ball i_ball = null)
    {
        if (i_ball == null)
        {
            i_ball = Instantiate(m_ballProto).GetComponent<Ball>();
        }
        m_defaultLauncher.RespawnBall(i_ball);
        OnResetDoors?.Invoke();
        BallsInPlay++;
        BallsRemaining--;
        Log("Spawned a ball; {0} in play, {1} remaining", BallsInPlay, BallsRemaining);
    }

    public void DestroyBalls()
    {
        var balls = GameObject.FindObjectsOfType<Ball>();
        foreach (var ball in balls)
        {
            Destroy(ball.gameObject);
        }
        BallsInPlay = 0;
    }

    public void ResetGame()
    {
        Log("Reset");
        OnResetGame?.Invoke();
        ScoreManager.Instance?.ResetScore();
        BallsRemaining = m_initialBallCount;
        DestroyBalls();
        OnResetSafeties?.Invoke();
        SpawnBall();
    }

    public void Log(string i_string, params object[] i_params)
    {
        if (LogGameState)
        {
            string text = string.Format(i_string, i_params);
            Debug.Log("Game: " + text);
        }
    }

    private int m_ballsRemaining;
    private float m_gameOverTimer = -1;
}
