using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class NPCActions : MonoBehaviour
{

    private Animator animator;

    public Animator Animator
    {
        get
        {
            return animator;
        }

        set
        {
            animator = value;
        }
    }

    void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    public void Stay()
    {
        Animator.SetBool("Moving", false);
        Animator.SetFloat("Velocity X", 0);
        Animator.SetFloat("Velocity Z", 0);
    }

    public void Moving()
    {
        Animator.SetBool("Moving", true);
        Animator.SetFloat("Velocity X", 0);
        Animator.SetFloat("Velocity Z", 6);

    }
}
