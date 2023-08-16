using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponentStalker : MovementComponent
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();

        //Pursue Player
        if (lastKnownPlayerLocation != Vector3.zero && self.Animator.GetBool("isChasing"))
        {
            Chase();
        }
    }

    public override void Patrol()
    {
        base.Patrol();
    }

    public override void GoToLocation(int waypointNumber)
    {
        base.GoToLocation(waypointNumber);
    }

    public override IEnumerator Wander(int wanderingTime)
    {
        StartCoroutine(base.Wander(wanderingTime));
        yield return null;
    }

    public override void Chase()
    {
        if (self.CanSeeTarget() && !self.IsInRange(5))
        {
            GetPlayerLocation();
        }
        else
        {
            self.Agent.SetDestination(lastKnownPlayerLocation);

            if (Vector3.Distance(lastKnownPlayerLocation, transform.position) < self.Accuracy)
            {
                if (!self.CanSeeTarget() && !self.IsInRange(5))
                {
                    self.Animator.SetBool("isChasing", false);
                    lastPlayerKnownLocations.Add(lastKnownPlayerLocation);
                    lastKnownPlayerLocation = Vector3.zero;
                }
            }
        }
    }

    public override void GetPlayerLocation()
    {
        base.GetPlayerLocation();
        lastKnownPlayerLocation = self.Player.transform.position;
    }
}
