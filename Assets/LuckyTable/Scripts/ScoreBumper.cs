using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBumper : MonoBehaviour
{
    public int m_value = 1;

    void OnCollisionEnter(Collision i_collision)
    {
        if (i_collision.collider.GetComponent<Ball>() != null)
        {
            ScoreManager.Instance?.AwardScore(m_value);
        }
    }

}
