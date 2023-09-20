using System;
using UnityEngine;
public class Enemy : Character
{
    [SerializeField] private float attackRange;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D rb;
    private Character target;
    public Character Target { get { return target; } }
    [SerializeField] private GameObject attackArea;

    // Start is called before the first frame update
    private IState currentState;
    private bool isRight = true;
    public override void OnInit()
    {
        base.OnInit();
        ChangeState(new IdleState());
        DeActiveAttack();   
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
        Destroy(healthBar.gameObject);
        Destroy(gameObject);
    }
    protected override void OnDeath()
    {
        ChangeState(null);
        base.OnDeath();
    }
    public void Moving()
    {
        ChangeAnim("Run");
        rb.velocity = transform.right * moveSpeed;

    }
    public void StopMoving()
    {
        ChangeAnim("Idle");
        rb.velocity = Vector2.zero;
    }
    public void Attack()
    {
        ChangeAnim("Attack");
        ActiveAttack();
        Invoke(nameof(DeActiveAttack), 0.5f);
    }
    private void Update()
    {
        if (currentState != null && !IsDead)
        {
            currentState.OnExecute(this);
        }
    }
    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
    public bool IsTargetInRange()
    {
        if(target != null)
        {
            return Vector2.Distance(target.transform.position, transform.position) <= attackRange;
        }
        return false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyWall")
        {
            ChangeDirection(!isRight);
        }
    }

    public void ChangeDirection(bool isRight)
    {
        this.isRight = isRight;
        transform.rotation = isRight ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(Vector3.up * 180);
    }

    internal void SetTarget(Character character)
    {
        this.target = character;
        if (IsTargetInRange())
        {
            ChangeState(new AttackState());
        }
        else if (Target != null)
        {
            ChangeState(new PatrolState());
        }
        else
        {
            ChangeState(new IdleState());

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
}
