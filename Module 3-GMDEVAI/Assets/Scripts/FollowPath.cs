using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    Transform goal;

    [SerializeField] private float speed;
    [SerializeField] private float accuracy;
    [SerializeField] private float rotSpeed;

    [SerializeField] private int startingWaypointNumber;

    private bool moveToLocation;

    public GameObject wpManager;
    GameObject[] wps;
    GameObject currentNode;

    int currentWaypointIndex = 0;
    Graph graph;

    // Start is called before the first frame update
    void Start()
    {
        wps = wpManager.GetComponent<WaypointManager>().waypoints;
        graph = wpManager.GetComponent<WaypointManager>().graph;

        startingWaypointNumber -= 1;

        currentNode = wps[startingWaypointNumber];
        goal = wps[startingWaypointNumber].transform;

        moveToLocation = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!moveToLocation)
        {
            return;
        }

        if(graph.getPathLength() == 0 || currentWaypointIndex == graph.getPathLength())
        {
            moveToLocation = false;
            return;
        }

        currentNode = graph.getPathPoint(currentWaypointIndex);

        if (Vector3.Distance(graph.getPathPoint(currentWaypointIndex).transform.position,
            transform.position) < accuracy)
        {
            currentWaypointIndex++;
        }

        if (currentWaypointIndex < graph.getPathLength())
        {
            goal = graph.getPathPoint(currentWaypointIndex).transform;
            Vector3 lookAtGoal = new Vector3(goal.position.x, transform.position.y, goal.position.z);
            Vector3 direction = lookAtGoal - this.transform.position;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);
            this.transform.Translate(0, 0, speed * Time.deltaTime);
        }
    }

    #region Location Functions

    public void GoToHelipad() => GoToLocation(2);

    public void GoToRuins() => GoToLocation(10);

    public void GoToFactory() => GoToLocation(8);

    public void GoToTwinMountain() => GoToLocation(3);

    public void GoToBarracks() => GoToLocation(9);

    public void GoToCommandCenter() => GoToLocation(12);

    public void GoToRefineryPump() => GoToLocation(5);

    public void GoToTankers() => GoToLocation(6);

    public void GoToRadar() => GoToLocation(13);

    public void GoToCommandPost() => GoToLocation(11);

    public void GoToMiddle() => GoToLocation(4);

    #endregion

    private void GoToLocation(int waypointNumber)
    {
        if (waypointNumber <= 0 || waypointNumber - 1 >= wps.Length)
        {
            Debug.LogWarning("Invalid Number:" + waypointNumber);
            return;
        }

        waypointNumber -= 1;
        moveToLocation = true;

        graph.AStar(currentNode, wps[waypointNumber]);
        currentWaypointIndex = 0;
    }
}
