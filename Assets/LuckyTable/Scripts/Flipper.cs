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

    // Update is called once per frame
    void FixedUpdate()
    {
        bool up = ((m_launchAction.action?.ReadValue<float>() ?? 0.0f) > 0.5f) ^ m_invert;

        var motor = m_hinge.motor;
        motor.force = m_force;
        motor.targetVelocity = (up ? 1.0f : -1.0f) * m_angularVelocity;
        m_hinge.motor = motor;
    }
}
