using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TiltSystem : MonoBehaviour
{
    public InputActionReference TiltControl;

    public float TiltScale = 2.0f;
    public float DownTurnSpeed = 1.0f;
    public float TiltH = 0.1f;
    public float TiltV = 0.05f;

    public Transform WorldObject;

    public Vector2 TiltTrackedValue { get; private set; }
    public Vector2 TiltSnappedValue { get; private set; }
    public Vector3 TargetDown { get; private set; }
    public Vector3 CurrentDown { get; private set; }

    public List<Vector2> TiltSnaps;

    // Start is called before the first frame update
    void Start()
    {
        TiltSnappedValue = Vector2.zero;
        TiltTrackedValue = TiltSnappedValue;
        TargetDown = Vector3.down;
        CurrentDown = TargetDown;
    }

    // Update is called once per frame
    void Update()
    {
        var tiltDelta = TiltScale * TiltControl.action?.ReadValue<Vector2>() ?? Vector2.zero;
        TiltTrackedValue += tiltDelta;
        TiltTrackedValue = new Vector2(Mathf.Clamp(TiltTrackedValue.x, -1.0f, 1.0f), Mathf.Clamp(TiltTrackedValue.y, -1.0f, 1.0f));

        foreach (var snap in TiltSnaps)
        {
            if ((TiltTrackedValue - snap).sqrMagnitude < (TiltTrackedValue - TiltSnappedValue).sqrMagnitude - 0.001f)
            {
                TiltSnappedValue = snap;
                TiltTrackedValue = TiltSnappedValue;
                TargetDown = new Vector3(snap.x * TiltH, -1.0f, snap.y * TiltV).normalized;
            }
        }
    }

    void FixedUpdate()
    {
        CurrentDown = Vector3.MoveTowards(CurrentDown, TargetDown, Time.fixedDeltaTime * DownTurnSpeed).normalized;
        //Debug.Log(CurrentDown.ToString());

        Physics.gravity = 9.81f * CurrentDown;

        if (WorldObject != null)
        {
            WorldObject.rotation = Quaternion.LookRotation(Vector3.Cross(Vector3.right, -CurrentDown), -CurrentDown);
        }
    }
}
