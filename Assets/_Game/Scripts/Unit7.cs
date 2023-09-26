using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit7 : MonoBehaviour
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
        currentPointIndex = Random.Range(0,3);
        target = points[currentPointIndex].position;
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
