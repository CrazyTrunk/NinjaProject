using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    Transform target;
    public Vector3 offset;
    public float Speed = 20;
    // Start is called before the first frame update
    //void Start()
    //{
    //    target = FindObjectOfType<Player>()?.transform;
    //}

    // Update is called once per frame
    void FixedUpdate()
    {
        if(target == null)
        {
            return;
        }
        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * Speed);
    }
    public void SetPlayerCamera()
    {
        target = FindObjectOfType<Player>()?.transform;
    }
}
