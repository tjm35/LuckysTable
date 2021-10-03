using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public bool LogScore = false;

    public int Score { get; private set; }

    public event Action<int, int> OnScoreChanged;

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
        OnScoreChanged?.Invoke(Score, oldScore);
    }

    public void ResetScore()
    {
        Score = 0;
    }
}
