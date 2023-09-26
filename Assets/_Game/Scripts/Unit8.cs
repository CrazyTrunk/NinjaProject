using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit8 : MonoBehaviour
{
    [SerializeField] public Transform aPoint, bPoint;
    public float speed = 5f;
    Vector3 target;
    bool isMoving = false;
    public float waitTime = 1.0f;

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
            yield return new WaitForSeconds(waitTime);
            isMoving = true;
            yield return new WaitForSeconds(waitTime);
            isMoving = false;
        }
    }
}
