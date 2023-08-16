using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChasers : AIBase
{
    [SerializeField] private float chaseRange;
    [SerializeField] private int chaseSpeed;

    public float ChaseSpeed { get { return chaseSpeed; } }
    public float ChaseRange { get { return chaseRange; } }

    protected override void Update()
    {
        if (IsInRange(chaseRange) && CanSeeTarget() && !animator.GetBool("isChasing"))
        {
            animator.SetBool("isChasing", true);
            animator.SetBool("isWandering", false);
            animator.SetBool("isPatrolling", false);
        }
    }
}
