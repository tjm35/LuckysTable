using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    public int DisplayMultiplier = 100;
    public bool LogScore = false;

    public int Score 
    { 
        get { return m_score; }
        private set
        {
            int oldScore = m_score;
            m_score = value;
            OnScoreChanged?.Invoke(Score, oldScore);
            OnScoreUpdate?.Invoke(Score);
            OnScoreUpdateDisplay?.Invoke(Score * DisplayMultiplier);
            if (m_score > HighScore)
            {
                HighScore = m_score;
                OnHighScoreUpdateDisplay?.Invoke(HighScore * DisplayMultiplier);
            }
        } 
    }

    public int HighScore { get; private set; }

    public event Action<int, int> OnScoreChanged;
    public UnityEvent<int> OnScoreUpdate;
    public UnityEvent<int> OnScoreUpdateDisplay;
    public UnityEvent<int> OnHighScoreUpdateDisplay;

    public static ScoreManager Instance;

    public ScoreManager()
    {
        Debug.Assert(Instance == null);
        Instance = this;
    }

    void Start()
    {
        ResetScore();
    }

    void OnDestroy()
    {
        Debug.Assert(Instance == this);
        Instance = null;
    }

    public void AwardScore(int i_amount)
    {
        int oldScore = Score;
        Score += i_amount;
        if (LogScore)
        {
            Debug.Log($"Awarded {i_amount} points, new score {Score}");
        }
    }

    public void ResetScore()
    {
        Score = 0;
    }

    private int m_score;
}
