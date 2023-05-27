using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TarvelToDestination : MonoBehaviour
{
    public Vector3 direction = new Vector3(8, 0, 4);
    public float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }
}
