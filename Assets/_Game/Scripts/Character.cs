using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private float hp;
    public bool IsDead => hp <= 0;
    private string CurrentAnim;

    // Start is called before the first frame update
    private void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void OnInit()
    {
        hp = 100;
    }
    public virtual void OnDespawn()
    {

    }
    protected virtual void OnDeath()
    {
        ChangeAnim("Die");
        Invoke(nameof(OnDespawn), 2f);
    }
    public void OnHit(float damage)
    {
        if( !IsDead)
        {
            hp -= damage;
            if(IsDead)
            {
                OnDeath();
            }
        }
    }


    protected void ChangeAnim(string animName)
    {
        if (CurrentAnim != animName)
        {
            animator.ResetTrigger(animName);
            CurrentAnim = animName;
            animator.SetTrigger(CurrentAnim);
        }
    }
}
