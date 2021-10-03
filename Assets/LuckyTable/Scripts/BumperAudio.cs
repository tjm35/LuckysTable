using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperAudio : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string HitSound;

    void OnCollisionEnter(Collision i_collision)
    {
        if (i_collision.collider.GetComponent<Ball>() != null)
        {
            if (!string.IsNullOrEmpty(HitSound))
            {
                FMODUnity.RuntimeManager.PlayOneShot(HitSound);
            }
        }
    }
}
