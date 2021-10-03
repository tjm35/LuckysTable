using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public FMODUnity.StudioEventEmitter m_rollingEmitter;

    public static List<Ball> ActiveBalls = new List<Ball>();

    [FMODUnity.EventRef]
    public string ImpactSound;

    public float m_minImpactSoundSpeed = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        m_rollingEmitter?.SetParameter("Speed", m_rigidbody.velocity.magnitude * Time.timeScale);
    }

    void OnCollisionEnter(Collision i_collision)
    {
        float speed = i_collision.relativeVelocity.magnitude;
        if (!string.IsNullOrEmpty(ImpactSound) && speed >= m_minImpactSoundSpeed)
        {
            var instance = FMODUnity.RuntimeManager.CreateInstance(FMODUnity.RuntimeManager.PathToGUID(ImpactSound));
            instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
            instance.setParameterByName("Speed", speed);
            instance.start();
            instance.release();
        }
    }

    void OnEnable()
    {
        ActiveBalls.Add(this);
    }

    void OnDisable()
    {
        ActiveBalls.Remove(this);
    }

    private Rigidbody m_rigidbody;
}
