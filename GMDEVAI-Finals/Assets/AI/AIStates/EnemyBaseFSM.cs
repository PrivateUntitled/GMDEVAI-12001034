using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseFSM : StateMachineBehaviour
{
    protected GameObject self;
    protected GameObject opponent;
    protected MovementComponent waypointComponent;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        self = animator.gameObject;
        opponent = self.GetComponent<AIBase>().Player;

        switch (self.GetComponent<AIBase>().AITypes)
        {
            case AITypes.BULLY:
                waypointComponent = self.GetComponent<MovementComponentBully>();
                waypointComponent.GetPlayerLocation();
                break;
            case AITypes.STALKER:
                waypointComponent = self.GetComponent<MovementComponentStalker>();
                waypointComponent.GetPlayerLocation();
                break;
            case AITypes.FRIEND:
                waypointComponent = self.GetComponent<MovementComponent>();
                waypointComponent.GetPlayerLocation();
                break;
            case AITypes.STUDENT:
                waypointComponent = self.GetComponent<MovementComponent>();
                waypointComponent.GetPlayerLocation();
                break;
            default:
                throw new System.Exception("Invalid AI Type");
        }
    }
}
