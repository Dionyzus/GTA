using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Destructable : MonoBehaviour
{

    [SerializeField] float hitPoints;
    [SerializeField] Image healthBar;

    private Animator animator;
    private bool flag;
    public event System.Action OnDeath;
    public event System.Action OnDamageReceived;

    float damageTaken;

    public float HitPointsRemaining
    {
        get
        {
            return hitPoints - damageTaken;
        }
    }
    public bool IsAlive
    {
        get
        {
            return HitPointsRemaining > 0;
        }
    }
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    public virtual void Die()
    {
        if (IsAlive)
            return;

        if (OnDeath != null)
        {
            OnDeath();
        }
    }
    public virtual void TakeDamage(float amount)
    {
        if (!IsAlive)
        {
            return;
        }
        if (amount > 0)
        {
            RandomHitAnimation();
        }
        damageTaken += amount;

        if (OnDamageReceived != null)
        {
            OnDamageReceived();
        }
        if (HitPointsRemaining <= 0)
        {
            Die();
        }
    }
    public void Reset()
    {
        damageTaken = 0;
    }
    public void RandomHitAnimation()
    {
        if (flag)
        {
            StartCoroutine(HitAnimationTime());
        }

        System.Random random = new System.Random();
        int randomHitNumber = random.Next(1, 3);
        //animator.Play("Hit1");
        if (randomHitNumber == 1)
        {
            animator.Play("Hit1");
            flag = true;

        }
        else
        {
            animator.Play("Hit2");
            flag = true;
        }
    }
    private void Update()
    {
        healthBar.fillAmount = HitPointsRemaining / hitPoints;
    }

    IEnumerator HitAnimationTime()
    {
        yield return new WaitForSeconds(1.5f);
        flag = false;
    }
}