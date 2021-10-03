using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraBallScoreAwards : MonoBehaviour
{
    public int BaseThreshold = 10;
    public int ThresholdMultiply = 2;
    public int ThresholdAdd = 0;

    public int NextBallThreshold { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        ScoreManager.Instance.OnScoreChanged += OnScoreChanged;
        Game.Instance.OnResetGame += OnResetGame;
        OnResetGame();
    }

    void OnResetGame()
    {
        NextBallThreshold = BaseThreshold;
    }

    void OnScoreChanged(int i_score, int i_oldScore)
    {
        if (i_score >= NextBallThreshold)
        {
            UpdateThreshold();
            Game.Instance?.ExtraBall();
        }
    }

    void UpdateThreshold()
    {
        NextBallThreshold = NextBallThreshold * ThresholdMultiply + ThresholdAdd;
        Game.Instance?.Log("Next ball threshold now {0}", NextBallThreshold);
    }
}
