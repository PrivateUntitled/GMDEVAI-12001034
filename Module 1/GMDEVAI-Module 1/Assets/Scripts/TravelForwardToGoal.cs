using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelForwardToGoal : MonoBehaviour
{
    public Transform goal;
    public float speed;

    public float rotSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 lookAtGoal = new Vector3(goal.position.x, this.transform.position.y, goal.position.z);

        Vector3 direction = lookAtGoal - this.transform.position;

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);

        if (Vector3.Distance(lookAtGoal, transform.position) > 1)
            transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
