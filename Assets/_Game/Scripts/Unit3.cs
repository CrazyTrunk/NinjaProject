using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Unit3 : MonoBehaviour
{
    [SerializeField] public Transform[] points;
    public float speed = 5f;
    Vector3 target;
    int currentPointIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = points[currentPointIndex].position;
        MoveNext();
    }

    private void MoveNext()
    {
        currentPointIndex++;
        if (currentPointIndex >= points.Length)
        {
            currentPointIndex = 0;
            transform.position = points[currentPointIndex].position;
            target = points[currentPointIndex].position;

        }
        else
        {
            target = points[currentPointIndex].position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            MoveNext();
        }
    }
}
