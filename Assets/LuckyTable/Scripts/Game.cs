using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Launcher m_defaultLauncher;

    public static Game Instance;

    public event Action OnResetSafeties;
    public event Action OnResetDoors;

    public Game()
    {
        Debug.Assert(Instance == null);
        Instance = this;
    }

    void OnDestroy()
    {
        Debug.Assert(Instance == this);
        Instance = null;
    }

    public void OnBallLeftPlay(Ball i_ball)
    {
        m_defaultLauncher.RespawnBall(i_ball);
        OnResetSafeties?.Invoke();
        OnResetDoors?.Invoke();
    }
}
