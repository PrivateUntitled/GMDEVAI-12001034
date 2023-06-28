using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int speed;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Camera cam;

    void FixedUpdate()
    {
        float xSpeed = 0;
        float ySpeed = 0;

        // Player Movement
        if (Input.GetKey(KeyCode.D))
        {
            xSpeed = speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            xSpeed -= speed;
        }
        if (Input.GetKey(KeyCode.W))
        {
            ySpeed = speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            ySpeed -= speed;
        }

        rb.velocity = new Vector3(xSpeed * Time.deltaTime, rb.velocity.y , ySpeed * Time.deltaTime);
    }
}
