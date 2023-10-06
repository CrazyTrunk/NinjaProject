using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : Character
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float Speed = 2;
    [SerializeField] private float JumpForce = 350;
    [SerializeField] private Kunai kunaiPrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private GameObject attackArea;

    private bool IsGrounded = true;
    private bool IsJumping = false;
    private bool IsAttack = false;
    private Vector3 savePoint;
    private float Horizontal;
    private int CoinCollect = 0;
    private void Awake()
    {
        CoinCollect = PlayerPrefs.GetInt("Coin", 0);
    }
    // Start is called before the first frame update
    void Update()
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
                IsJumping = false; 
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
        if (!IsGrounded && rb.velocity.y <= 0.1f)
        {
            ChangeAnim("Fall");
            IsJumping = false;
        }

        if (Mathf.Abs(Horizontal) > 0.1f)
        {
            rb.velocity = new Vector2(Horizontal * Speed, rb.velocity.y);
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
        IsAttack = false;
        transform.position = savePoint;
        ChangeAnim("Idle");
        DeActiveAttack();
        SavePoint();
        UIManager.instance.SetCoin(CoinCollect);
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
    }
    protected override void OnDeath()
    {
        base.OnDeath();
        OnInit();
    }
    private bool CheckGrounded()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 1.1f, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);
        return hit.collider != null;
    }
    public void Attack()
    {
        ChangeAnim("Attack");
        IsAttack = true;
        Invoke(nameof(ResetAttack), 0.5f);
        ActiveAttack();
        Invoke(nameof(DeActiveAttack), 0.5f);
    }
    public void Throw()
    {
        ChangeAnim("Throw");
        IsAttack = true;
        Invoke(nameof(ResetAttack), 0.5f);
        //vi tri va goc xoay
        Instantiate(kunaiPrefab, throwPoint.position, throwPoint.rotation);
    }
    public void Jump()
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
            CoinCollect++;
            PlayerPrefs.SetInt("Coin", CoinCollect);
            UIManager.instance.SetCoin(CoinCollect);
            Destroy(collision.gameObject);

        }
        if (collision.tag == "DeathZone")
        {
            ChangeAnim("Die");
            Invoke(nameof(OnInit), 1f);
        }
    }
    private void ActiveAttack()
    {
        attackArea.SetActive(true);
    }
    private void DeActiveAttack()
    {
        attackArea.SetActive(false);
    }
    internal void SavePoint()
    {
        savePoint = transform.position;
    }
    public void SetSavePointPlayer(Vector3 position)
    {
        savePoint = position;
    }
    public void SetMove(float horizontal)
    {
        this.Horizontal = horizontal;
    }
}
