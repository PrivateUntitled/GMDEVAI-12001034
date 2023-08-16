using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    protected AIBase self;

    protected GameObject[] wp;
    protected Graph graph = new Graph();

    protected Vector3 goal;
    protected GameObject currentNode;
    protected int currentWaypointIndex = 0;

    protected List<Vector3> lastPlayerKnownLocations = new List<Vector3>();
    
    protected int chosenLocation;

    public GameObject[] Wp { get { return wp; } }
    public GameObject CurrentNode { set { currentNode = value; } }

    protected Vector3 lastKnownPlayerLocation = Vector3.zero;

    public Vector3 LastKnownPlayerLocation { set { lastKnownPlayerLocation = value; } }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        wp = WaypointManager.instance.Waypoints;

        self = GetComponent<AIBase>();

        if (WaypointManager.instance.Waypoints.Length > 0)
        {
            foreach (GameObject wp in wp)
            {
                graph.AddNode(wp);
            }

            foreach (Link l in WaypointManager.instance.Links)
            {
                graph.AddEdge(l.node1, l.node2);
                if (l.dir == Link.direction.BI)
                {
                    graph.AddEdge(l.node2, l.node1);
                }
            }
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        graph.debugDraw();
    }

    protected virtual void LateUpdate()
    {
        if (self.Animator.GetBool("isPatrolling"))
        {
            if(graph.getPathLength() != 0 && currentWaypointIndex != graph.getPathLength())
            {
                Patrol();
            }
            else
            {
                Debug.Log("Check");

                self.Agent.SetDestination(lastPlayerKnownLocations[chosenLocation]);

                if (Vector3.Distance(lastPlayerKnownLocations[chosenLocation],
                            transform.position) < self.Accuracy)
                {
                    self.Animator.SetBool("isPatrolling", false);
                }
            }
        }
    }

    #region Patrol Functions
    public virtual void Patrol()
    {
        currentNode = graph.getPathPoint(currentWaypointIndex);

        if (Vector3.Distance(graph.getPathPoint(currentWaypointIndex).transform.position,
                            transform.position) < self.Accuracy)
        {
            currentWaypointIndex++;
        }

        if (currentWaypointIndex < graph.getPathLength())
        {
            goal = graph.getPathPoint(currentWaypointIndex).transform.position;
            self.Agent.SetDestination(goal);
        }
        else
        {
            self.Animator.SetBool("isPatrolling", false);
        }
    }

    public virtual void GoToLocation(int waypointNumber)
    {
        int randomPatrolBehaviour = Random.Range(0, 2);

        if (randomPatrolBehaviour == 0 || lastPlayerKnownLocations.Count == 0)
        {
            if (waypointNumber < 0 || waypointNumber >= wp.Length)
            {
                Debug.LogError("Invalid Waypoint Number");
                return;
            }

            graph.AStar(currentNode, wp[waypointNumber]);
            currentWaypointIndex = 0;
        }
        else
        {
            chosenLocation = Random.Range(0, lastPlayerKnownLocations.Count);
        }
    }
    #endregion

    #region Wander Functions
    public virtual IEnumerator Wander(int wanderingTime)
    {
        yield return new WaitForSeconds(wanderingTime);
        self.Animator.SetBool("isWandering", false);
    }
    #endregion

    #region Chase Functions
    public virtual void Chase() { }

    public virtual void GetPlayerLocation() { }
    #endregion
}
