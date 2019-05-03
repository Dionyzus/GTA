using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PathFinder))]
[RequireComponent(typeof(EnemyPlayer))]
public class EnemyAnimation : MonoBehaviour {

    [SerializeField] Animator animator;

    Vector3 lastPosition;

    PathFinder pathFinder;
    EnemyPlayer enemyPlayer;

    private bool m_IsCrouched;
    public bool IsCrouched
    {
        get
        {
            return m_IsCrouched;
        }
        internal set
        {
            m_IsCrouched = true;
            GameManager.Instance.Timer.Add(CheckIsSafeToStandUp, 15);
        }
    }
    void Awake()
    {
        pathFinder = GetComponent<PathFinder>();
        enemyPlayer = GetComponent<EnemyPlayer>();
    }
    void Update()
    {
        float velocity = ((transform.position - lastPosition).magnitude) / Time.deltaTime;
        lastPosition = transform.position;
        animator.SetBool("IsRunning", enemyPlayer.EnemyState.CurrentMode == EnemyState.EMode.AWARE);
        animator.SetFloat("Vertical", velocity / pathFinder.Agent.speed);
        animator.SetBool("IsAiming", enemyPlayer.EnemyState.CurrentMode == EnemyState.EMode.AWARE);
        animator.SetBool("IsCrouched", IsCrouched);

    }
    void CheckIsSafeToStandUp()
    {
        bool isUnaware = enemyPlayer.EnemyState.CurrentMode == EnemyState.EMode.UNAWARE;

        if(isUnaware)
        {
            IsCrouched = false;
        }
    }
}
