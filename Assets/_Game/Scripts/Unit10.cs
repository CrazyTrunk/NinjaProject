using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit10 : MonoBehaviour
{
    [SerializeField] public Transform aPoint, bPoint;
    public float speed = 5f;
    Vector3 target;
    bool isMoving = true;
    public float waitTime = 5.0f;
    bool isWaiting = false;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = Vector3.Lerp(aPoint.position, bPoint.position, 0.5f);
        target = bPoint.position;
        StartCoroutine(SetMove());
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, bPoint.position) < 0.1f)
            {
                target = aPoint.position;
            }
            else if (Vector2.Distance(transform.position, aPoint.position) < 0.1f)
            {
                target = bPoint.position;
            }
        }

    }
    IEnumerator SetMove()
    {
        while (true)
        {
            if(!isWaiting &&  Vector2.Distance(transform.position, aPoint.position) < 0.1f)
            {
                isMoving = false;
                float randomTime = Random.Range(1f, waitTime);
                isWaiting = true;
                yield return new WaitForSeconds(randomTime);
                isWaiting = false;
                isMoving = true;
            }
            yield return null;
        }
    }
}
