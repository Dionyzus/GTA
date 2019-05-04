using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDoll_TEST : Destructable {

    [SerializeField] SpawnPoint[] spawnPoints;
    public Animator animator;
    private Rigidbody[] bodyParts;
    private MoveController moveController;

    void Start()
    {
        bodyParts = transform.GetComponentsInChildren<Rigidbody>();
        EnableRagdoll(false);
        moveController = GetComponent<MoveController>();
    }

    void SpawnAtNewSpawnpoint()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        transform.position = spawnPoints[spawnIndex].transform.position;
        transform.rotation = spawnPoints[spawnIndex].transform.rotation;
    }
    public override void Die()
    {
        base.Die();
        EnableRagdoll(true);
        animator.enabled = false;

        GameManager.Instance.Timer.Add(() =>
        {
            EnableRagdoll(false);
            SpawnAtNewSpawnpoint();
            animator.enabled = true;
            Reset();
        }, 5);

    }

    void Update()
    {
        if(!IsAlive)
        {
            return;
        }
        
        animator.SetFloat("Vertical", 1);
        moveController.Move(new Vector2(3, 0));
    }

    void EnableRagdoll(bool value)
    {
        for (int i = 0; i < bodyParts.Length; i++)
        {
            bodyParts[i].isKinematic = !value;
        }
    }
}
