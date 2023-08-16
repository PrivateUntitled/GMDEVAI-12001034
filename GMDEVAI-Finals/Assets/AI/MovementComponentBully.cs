using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponentBully : MovementComponent
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
        if (self.CanSeeTarget())
        {
            self.Agent.speed = self.GetComponent<AIChasers>().ChaseSpeed;
            GetPlayerLocation();
            
            self.Agent.SetDestination(lastKnownPlayerLocation);
        }
        else 
        {
            self.Agent.speed = self.NormalSpeed;
            lastPlayerKnownLocations.Add(lastKnownPlayerLocation);
            self.Animator.SetBool("isChasing", false);
        }
    }

    public override void GetPlayerLocation()
    {
        Vector3 targetDirection = self.Player.transform.position - this.transform.position;

        float lookAhead = targetDirection.magnitude / self.Agent.speed + self.Player.GetComponent<FirstPersonMovement>().Speed;

        lastKnownPlayerLocation = self.Player.transform.position + self.Player.transform.forward * lookAhead;
    }
}
