using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private int rotSpeed;

    [SerializeField] private Camera cam;

    void FixedUpdate()
    {
        // Player Movement
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime, this.transform);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime, this.transform);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime, this.transform);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime, this.transform);
        }

        // Player Rotation
        cam.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(cam.transform.rotation.x, Input.mousePosition.x + Screen.width / 2, cam.transform.rotation.z), rotSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(transform.rotation.x, Input.mousePosition.x + Screen.width / 2, transform.rotation.z);
    }
}
