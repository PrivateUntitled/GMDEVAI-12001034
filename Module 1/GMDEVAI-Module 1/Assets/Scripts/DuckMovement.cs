using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckMovement : MonoBehaviour
{
    [SerializeField] private Transform goal;
    [SerializeField] private float speed;
    [SerializeField] private float rotSpeed;

    void LateUpdate()
    {
        // Calculate Location
        Vector3 lookAtGoal = new Vector3(goal.position.x, this.transform.position.y, goal.position.z);
        Vector3 direction = lookAtGoal - this.transform.position;

        // Duck Rotates (using Slerp)
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);

        // Duck goes to Position (using Lerp)
        this.transform.position = Vector3.Lerp(this.transform.position, lookAtGoal, Time.deltaTime * speed);
    }
}
