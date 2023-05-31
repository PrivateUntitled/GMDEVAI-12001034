using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    [SerializeField] private Transform goal;

    
    [SerializeField] private float speed;
    [SerializeField] private float rotSpeed;

    [SerializeField] private float acceleation;
    [SerializeField] private float decceleration;

    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;

    [SerializeField] private float breakAngle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {

        Vector3 lookAtGoal = new Vector3(goal.transform.position.x, goal.transform.position.y, goal.transform.position.z);

        Vector3 direction = lookAtGoal - this.transform.position;

        float deltaY = this.transform.position.y - goal.transform.position.y;

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);



        if (Vector3.Angle(goal.forward, this.transform.forward) > breakAngle && speed > 2)
        {
            speed = Mathf.Clamp(speed - (decceleration * Time.deltaTime), minSpeed, maxSpeed);
        }
        else
        {
            speed = Mathf.Clamp(speed + (acceleation * Time.deltaTime), minSpeed, maxSpeed);
        }

        if (deltaY < 0 && speed > 10)
        {
            speed = Mathf.Clamp(speed + (decceleration * deltaY * Time.deltaTime), minSpeed, maxSpeed);
            Debug.Log("Deccelerating " + (decceleration * deltaY * Time.deltaTime));
        }
        else if (deltaY > 0 && speed > 10)
        {
            speed = Mathf.Clamp(speed + (acceleation * deltaY * Time.deltaTime), minSpeed, maxSpeed);
            Debug.Log("Accelerating " + (acceleation * deltaY * Time.deltaTime));
        }
        else
        {
            speed = Mathf.Clamp(speed + (acceleation * Time.deltaTime), minSpeed, maxSpeed);
        }

        this.transform.Translate(0, 0, Time.deltaTime * speed);
    }
}
