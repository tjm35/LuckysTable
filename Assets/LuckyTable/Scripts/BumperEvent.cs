using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BumperEvent : MonoBehaviour
{
    public UnityEvent OnBumperHit;

    void OnCollisionEnter(Collision i_collision)
    {
        if (i_collision.collider.GetComponent<Ball>() != null)
        {
            OnBumperHit?.Invoke();
        }
    }
}
