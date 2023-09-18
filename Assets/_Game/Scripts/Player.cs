using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float Speed = 200;
    [SerializeField] private Animator animator;
    [SerializeField] private float JumpForce = 350;

    private bool IsGrounded = true;
    private bool IsJumping = false;
    private bool IsAttack = false;
    private string CurrentAnim;
    private float Horizontal;
    // Start is called before the first frame update
    void Start()
    {

    }
    //Update is called once per frame
    void FixedUpdate()
    {

        if (!IsGrounded && rb.velocity.y < 0)
        {
            ChangeAnim("Fall");
        }
        if (IsGrounded)
        {
            IsJumping = false;
        }
    }

    void Update()
    {
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

            //Jump
            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
            {
                Jump();
            }

            if (Math.Abs(Horizontal) > 0.1f)
            {
                ChangeAnim("Run");
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
        IsJumping = true;
        ChangeAnim("Jump");
        rb.AddForce(JumpForce * Vector2.up);
    }
    private void ResetAttack()
    {
        IsAttack = false;

        ChangeAnim("Idle");
    }
    private void ChangeAnim(string animName)
    {
        if (CurrentAnim != animName)
        {
            animator.ResetTrigger(animName);
            CurrentAnim = animName;
            animator.SetTrigger(CurrentAnim);
        }
    }
}
