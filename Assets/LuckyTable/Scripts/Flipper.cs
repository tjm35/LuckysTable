using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Flipper : MonoBehaviour
{
    public HingeJoint m_hinge;
    public float m_angularVelocity = 300.0f;
    public float m_force = 1.0f;
    public bool m_invert = false;
    public InputActionReference m_launchAction;

    [FMODUnity.EventRef]
    public string FlipSound;

    // Update is called once per frame
    void FixedUpdate()
    {
        bool pressed = (m_launchAction?.action?.ReadValue<float>() ?? 0.0f) > 0.5f;
        if (pressed && !m_lastPressed && !string.IsNullOrEmpty(FlipSound))
        {
            FMODUnity.RuntimeManager.PlayOneShot(FlipSound);
        }
        m_lastPressed = pressed;
        bool up = pressed ^ m_invert;

        var motor = m_hinge.motor;
        motor.force = m_force;
        motor.targetVelocity = (up ? 1.0f : -1.0f) * m_angularVelocity;
        m_hinge.motor = motor;
    }

    private bool m_lastPressed = false;
}
