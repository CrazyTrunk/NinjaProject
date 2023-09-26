using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit4 : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public Transform controlPoint;
    public float duration = 2.0f;

    private float t = 0.0f;
    private bool reachedControlPoint = false;

    private void Update()
    {
        if (t < 1.0f)
        {
            t += Time.deltaTime / (duration * 0.5f);  // Divide by half of the duration for each segment

            if (!reachedControlPoint)
            {
                transform.position = Vector3.Lerp(pointA.position, controlPoint.position, t);
                if (t >= 1.0f)
                {
                    t = 0;  // Reset for next lerp
                    reachedControlPoint = true;
                }
            }
            else
            {
                transform.position = Vector3.Lerp(controlPoint.position, pointB.position, t);
            }
        }
    }

    Vector3 GetBezierPositionUsingLerp(Vector3 start, Vector3 control, Vector3 end, float t)
    {
        Vector3 lerpAC = Vector3.Lerp(start, control, t);
        Vector3 lerpCB = Vector3.Lerp(control, end, t);
        return Vector3.Lerp(lerpAC, lerpCB, t);
    }
}
