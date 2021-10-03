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
        }
    }

    void Disable()
    {
        InputActionMap.Disable();
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

	private InputActionMap InputActionMap => inputAsset.FindActionMap(actionMap);

}
