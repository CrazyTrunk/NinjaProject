using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float Speed = 200;
    [SerializeField] private float JumpForce = 350;

    private bool IsGrounded = true;
    private bool IsJumping = false;
    private bool IsAttack = false;
    private bool IsDead = false;
    private Vector3 savePoint;
    private float Horizontal;
    private int CoinCollect = 0;
    // Start is called before the first frame update
    void Start()
    {
        SavePoint();
    }
    void LateUpdate()
    {
        if (IsDead)
        {
            return;
        }
        IsGrounded = CheckGrounded();
        Horizontal = Input.GetAxisRaw("Horizontal");

        if (IsAttack)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        if (IsGrounded)
        {
            if (IsJumping)
            {
                return;
            }

            if (Math.Abs(Horizontal) > 0.1f)
            {
                ChangeAnim("Run");
            }
            //Jump
            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
            {
                Jump();
            }
            //Attack
            if (Input.GetKeyDown(KeyCode.Z) && IsGrounded)
            {
                Attack();
            }
            //Throw

            if (Input.GetKeyDown(KeyCode.X) && IsGrounded)
            {
                Throw();
            }
        }

        if (!IsGrounded && rb.velocity.y < 0)
        {
            ChangeAnim("Fall");
            IsJumping = false; 
        }

        if (Math.Abs(Horizontal) > 0.1f)
        {
            rb.velocity = new Vector2(Horizontal * Time.fixedDeltaTime * Speed, rb.velocity.y);
            //horizon 
            transform.rotation = Quaternion.Euler(new Vector3(0, Horizontal > 0 ? 0 : 180, 0));
            //transform.localScale = new Vector3(Horizontal, 1, 1);
        }
        else if (IsGrounded && !IsAttack)
        {
            ChangeAnim("Idle");
            rb.velocity = Vector2.zero;
        }
    }
    public override void OnInit()
    {
        base.OnInit();  
        IsDead = false;
        IsAttack = false;
        transform.position = savePoint;
        ChangeAnim("Idle");
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
    }
    protected override void OnDeath()
    {
        base.OnDeath();
    }
    private bool CheckGrounded()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 1.1f, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);
        return hit.collider != null;
    }
    private void Attack()
    {
        ChangeAnim("Attack");
        IsAttack = true;
        Invoke(nameof(ResetAttack), 0.5f);
    }
    private void Throw()
    {
        ChangeAnim("Throw");
        IsAttack = true;
        Invoke(nameof(ResetAttack), 0.5f);

    }
    private void Jump()
    {
        ChangeAnim("Jump");
        rb.AddForce(JumpForce * Vector2.up);
        IsJumping = true;
    }
    private void ResetAttack()
    {
        IsAttack = false;

        ChangeAnim("Idle");
    }

    //player va cham vs do vat dung Ham
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Coin")
        {
            Destroy(collision.gameObject);
            CoinCollect++;
        }
        if (collision.tag == "DeathZone")
        {
            ChangeAnim("Die");
            IsDead = true;
            Invoke(nameof(OnInit), 1f);
        }
    }

    internal void SavePoint()
    {
        savePoint = transform.position;
    }
}
