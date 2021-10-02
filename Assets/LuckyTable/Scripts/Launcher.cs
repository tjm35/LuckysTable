using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Launcher : MonoBehaviour
{
    public Rigidbody m_cylinder;
    public BoxCollider m_launchZone;
    public Transform m_ballSpawnPoint;
    public float m_pullDist = 0.01f;
    public float m_maxPullTime = 1.0f;
    public float m_releaseTime = 0.1f;
    public AnimationCurve m_launchForceCurve;

    public InputActionReference m_launchAction;

    // Start is called before the first frame update
    void Start()
    {
        m_cylinderStartZ = m_cylinder.transform.localPosition.z;
        m_pullAmount = 0.0f;
        m_lastPullState = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ((m_launchAction.action?.ReadValue<float>() ?? 0.0f) > 0.5f)
        {
            m_pullAmount += Time.fixedDeltaTime / m_maxPullTime;
            m_lastPullState = true;
        }
        else
        {
            m_pullAmount -= Time.fixedDeltaTime / m_releaseTime;
            if (m_lastPullState == true)
            {
                DoLaunch();
            }
            m_lastPullState = false;
        }
        m_pullAmount = Mathf.Clamp01(m_pullAmount);

        float targetPos = m_cylinderStartZ - m_pullAmount * m_pullDist;

        m_cylinder.transform.localPosition = targetPos * Vector3.forward;
    }

    void DoLaunch()
    {
        var launchables = Physics.OverlapBox(m_launchZone.transform.position + (m_launchZone.transform.rotation * m_launchZone.center), 0.5f * m_launchZone.size, m_launchZone.transform.rotation);
        foreach (var collider in launchables)
        {
            var rb = collider.GetComponent<Rigidbody>();
            if (rb != null && rb.isKinematic == false)
            {
                rb.AddForce(GetLaunchForce(m_pullAmount) * transform.forward);
            }
        }
    }

    float GetLaunchForce(float i_pullAmount)
    {
        return m_launchForceCurve.Evaluate(i_pullAmount);
    }

    public void RespawnBall(Ball i_ball)
    {
        var rb = i_ball.GetComponent<Rigidbody>();
        rb.position = m_ballSpawnPoint?.position ?? transform.position;
        rb.velocity = Vector3.zero;
        rb.ResetInertiaTensor();
    }

    private float m_pullAmount;
    private float m_cylinderStartZ;
    private bool m_lastPullState;
}
