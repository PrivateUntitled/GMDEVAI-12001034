using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : MonoBehaviour
{
    private ShootComponent shootComponent;

    // Start is called before the first frame update
    void Start()
    {
        shootComponent = this.GetComponent<ShootComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            shootComponent.StartFiring();

        if (Input.GetMouseButtonUp(0))
            shootComponent.StopFiring();
    }
}
