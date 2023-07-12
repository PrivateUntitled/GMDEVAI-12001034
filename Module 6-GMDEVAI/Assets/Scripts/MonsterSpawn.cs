using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    [SerializeField] private GameObject obstacle;
    [SerializeField] private GameObject goal;

    GameObject[] agents;

    // Start is called before the first frame update
    void Start()
    {
        agents = GameObject.FindGameObjectsWithTag("agent");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                Instantiate(obstacle, hit.point, obstacle.transform.rotation);
                foreach(GameObject a in agents)
                {
                    a.GetComponent<AIControl>().DetectNewObstacle(hit.point);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                Instantiate(goal, hit.point, goal.transform.rotation);
                foreach (GameObject a in agents)
                {
                    a.GetComponent<AIControl>().DetectNewGoal(hit.point);
                }
            }
        }
    }
}
