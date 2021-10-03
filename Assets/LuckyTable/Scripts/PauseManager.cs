using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    public InputActionReference StartControl;
    public InputActionReference PauseControl;
    public bool StartPaused = true;

    public bool Paused { get; private set; }

    public UnityEvent OnPause;
    public UnityEvent OnUnpause;

    void Start()
    {
        if (StartPaused)
        {
            Pause();
        }
        else
        {
            Unpause();
        }
    }

    void OnEnable()
    {
        StartControl.action.performed += OnStartPressed;
        PauseControl.action.performed += OnPausePressed;
    }

    void OnDisable()
    {
        StartControl.action.performed -= OnStartPressed;
    }

    void Update()
    {
        if (!Paused && Screen.fullScreen == false && !Application.isEditor)
        {
            Pause();
        }
    }

    private void OnStartPressed(InputAction.CallbackContext _)
    {
        //Debug.Log("OnStartPressed");
        Unpause();
    }

    private void OnPausePressed(InputAction.CallbackContext _)
    {
        if (Paused)
        {
            Unpause();
        }
        else
        {
            Pause();
        }
    }

    private void Pause()
    {
        //Debug.Log("Pausing");
        Time.timeScale = 0.0f;
        Paused = true;
        OnPause?.Invoke();
    }

    private void Unpause()
    {
        //Debug.Log("Unpausing");
        Time.timeScale = 1.0f;
        Paused = false;
        OnUnpause?.Invoke();
    }
}
