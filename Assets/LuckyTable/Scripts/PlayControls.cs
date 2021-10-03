using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayControls : MonoBehaviour
{
    public InputActionAsset inputAsset;
    public string actionMap;
    public bool lockCursor = false;

    void OnEnable()
    {
        InputActionMap.Enable();
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Screen.fullScreen = true;
        }
    }

    void Disable()
    {
        InputActionMap.Disable();
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Screen.fullScreen = false;
        }
    }

	private InputActionMap InputActionMap => inputAsset.FindActionMap(actionMap);

}
