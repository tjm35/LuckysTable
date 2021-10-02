using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopBumper : MonoBehaviour
{
    public float m_force = 1.0f;

    void OnCollisionEnter(Collision i_collision)
    {
        if (i_collision.collider.GetComponent<Ball>() != null)
        {
            if (i_collision.contactCount > 0)
            {
                //Debug.Log("Bumper");
                Vector3 resolutionDir = i_collision.GetContact(0).normal;
                i_collision.collider.GetComponent<Rigidbody>()?.AddForce(resolutionDir * m_force);
            }
            else
            {
                Debug.Log("Collision has no contacts");
            }
        }
    }

}
