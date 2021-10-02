using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Safety : MonoBehaviour
{
    public Transform m_retractor;
    public float m_retractDistance = 0.018f;
    public float m_retractTime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_startPos = m_retractor.localPosition;
        m_collider = GetComponent<Collider>();
        Game.Instance.OnResetSafeties += ResetSafety;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float currentHeight = m_retractor.localPosition.y - m_startPos.y;
        float targetHeight = currentHeight;
        if (m_shouldRetract)
        {
            targetHeight -= m_retractDistance * Time.fixedDeltaTime / m_retractTime;
        }
        else
        {
            targetHeight += m_retractDistance * Time.fixedDeltaTime / m_retractTime;
        }
        targetHeight = Mathf.Clamp(targetHeight, -m_retractDistance, 0.0f);

        m_collider.enabled = (targetHeight > -0.5f * m_retractDistance);

        m_retractor.localPosition = new Vector3(m_startPos.x, m_startPos.y + targetHeight, m_startPos.z);
    }

    void OnCollisionEnter(Collision i_collision)
    {
        if (i_collision.collider.GetComponent<Ball>() != null)
        {
            if (!m_shouldRetract)
            {
                m_shouldRetract = true;
            }
        }
    }

    public void ResetSafety()
    {
        m_shouldRetract = false;
    }

    private Vector3 m_startPos;
    private bool m_shouldRetract = false;
    private Collider m_collider;
}
