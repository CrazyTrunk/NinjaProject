using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform aPoint, bPoint;
    Vector3 target;
    [SerializeField] private float Speed;

    // Start is called before the first frame update
    void Start()
    {
        //khoi tao cai thanh = vi tri cua diem A
        transform.position  = aPoint.position;
        //dich den se la vi tri cua diem B
        target = bPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        //thuc hien viec update
        // vi tri moi = diem hien tai => vi tri diem B * delta
        transform.position = Vector3.MoveTowards(transform.position, target, Speed * Time.deltaTime);
        //neu khoang cach cua diem hien tai va diem A nho hon 0.1
        if(Vector2.Distance(transform.position, aPoint.position) < 0.1f)
        {
            //dich den se la diem B
            target = bPoint.position;
        }else if(Vector2.Distance(transform.position, bPoint.position) < 0.1f)
        {
            target = aPoint.position;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //set doi tuong moving nay khi gap player thang player se thanh thang con va cung di chuyen
        if(collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(transform);
        }
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null);
        }
    }
}
