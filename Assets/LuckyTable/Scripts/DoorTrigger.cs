using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DoorTrigger : MonoBehaviour
{
    public Transform m_extender;
    public float m_extendDistance = 0.018f;
    public float m_extendTime = 0.0f;
    public bool m_startTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        m_startPos = m_extender.localPosition;
        m_collider = m_extender.GetComponent<Collider>();
        ResetDoor();
        Game.Instance.OnResetDoors += ResetDoor;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float currentHeight = m_extender.localPosition.y - m_startPos.y;
        float targetHeight = currentHeight;
        if (m_shouldExtend)
        {
            targetHeight += m_extendDistance * Time.fixedDeltaTime / m_extendTime;
        }
        else
        {
            targetHeight -= m_extendDistance * Time.fixedDeltaTime / m_extendTime;
        }
        targetHeight = Mathf.Clamp(targetHeight, 0.0f, m_extendDistance);

        m_collider.enabled = (targetHeight > 0.5f * m_extendDistance);

        m_extender.localPosition = new Vector3(m_startPos.x, m_startPos.y + targetHeight, m_startPos.z);
    }

    void OnTriggerEnter(Collider i_collider)
    {
        //Debug.Log("DoorTrigger");
        if (i_collider.GetComponent<Ball>() != null)
        {
            if (!m_shouldExtend)
            {
                m_shouldExtend = true;
            }
        }
    }

    public void ResetDoor()
    {
        m_shouldExtend = m_startTriggered;
    }

    private Vector3 m_startPos;
    private bool m_shouldExtend = false;
    private Collider m_collider;
}
