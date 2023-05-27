using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckMovement : MonoBehaviour
{
    [SerializeField] private Transform goal;
    [SerializeField] private float speed;
    [SerializeField] private float rotSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void LateUpdate()
    {
        Vector3 lookAtGoal = new Vector3(goal.position.x, this.transform.position.y, goal.position.z);

        Vector3 direction = lookAtGoal - this.transform.position;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);

        this.transform.position = Vector3.Lerp(this.transform.position, lookAtGoal, Time.deltaTime * speed);
    }
}
