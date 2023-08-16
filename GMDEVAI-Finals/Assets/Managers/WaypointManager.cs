using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Link
{
    public enum direction { UNI, BI };
    public GameObject node1;
    public GameObject node2;
    public direction dir;
}

public class WaypointManager : Singleton<WaypointManager>
{
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private Link[] links;

    public GameObject[] Waypoints { get { return waypoints; } }
    public Link[] Links { get { return links; } }

    // Start is called before the first frame update
    void Start()
    {
        CheckList();
    }

    public void CheckList()
    {
        /*for (int i = 0; i < links.Length; i++)
        {
            for(int j = 0; j < links.Length; j++)
            {
                if (links[i].node1 == links[j].node1 &&
                    links[i].node2 == links[j].node2 &&
                    links[i].dir == links[j].dir)
                {
                    links.RemoveAt(i);
                    CheckList();
                }
            }
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
