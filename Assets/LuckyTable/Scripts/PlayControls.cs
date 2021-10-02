using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayControls : MonoBehaviour
{
    public InputActionAsset inputAsset;
    public string actionMap;

    void OnEnable()
    {
        InputActionMap.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Screen.fullScreen = true;
    }

    void Disable()
    {
        InputActionMap.Disable();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Screen.fullScreen = false;
    }

	private InputActionMap InputActionMap => inputAsset.FindActionMap(actionMap);

}
