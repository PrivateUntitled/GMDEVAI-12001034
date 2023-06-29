using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hider : AIControl
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(canSeeTarget());

        if (!isInRange())
        {
            Debug.Log("Wander");
            Wander();
        }
            

        if (canSeeTarget() && isInRange())
        {
            Debug.Log("Hide");
            CleverHide();
        }
    }
}
