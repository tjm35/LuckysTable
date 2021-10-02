using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidZone : MonoBehaviour
{
    public Game m_game;

    void OnTriggerExit(Collider i_collider)
    {
        if (i_collider.GetComponent<Ball>())
        {
            m_game?.OnBallLeftPlay(i_collider.GetComponent<Ball>());
        }
    }
}
