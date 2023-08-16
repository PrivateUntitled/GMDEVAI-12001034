using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AITypes
{
    STUDENT,
    STALKER,
    BULLY,
    FRIEND
}

public class AIBase : MonoBehaviour
{
    [SerializeField] private float accuracy;
    [SerializeField] private int normalSpeed;
    [SerializeField] private AITypes aITypes;

    private NavMeshAgent agent;
    protected Animator animator;

    public Animator Animator { get { return animator; } }

    private GameObject player;

    public GameObject Player { get { return player; } }
    public float Accuracy { get { return accuracy; } }

    public NavMeshAgent Agent { get { return agent; } }
    public int NormalSpeed { get { return normalSpeed; } }

    public AITypes AITypes { get { return aITypes; } }

    public void Init(GameObject _player)
    {
        player = _player;
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = normalSpeed;
        Debug.Log(agent.speed);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public bool CanSeeTarget()
    {
        RaycastHit raycastInfo;
        Vector3 rayToTarget = player.transform.position - this.transform.position;
        rayToTarget.y += 1.0f;

        if (Physics.Raycast(this.transform.position, rayToTarget, out raycastInfo))
        {
            Debug.DrawRay(transform.position, rayToTarget, Color.green);
            return raycastInfo.transform.gameObject.tag == "Player";
        }

        return false;
    }

    public bool IsInRange(float range)
    {
        float spotDistance = Mathf.Abs(Vector3.Distance(this.transform.position, player.transform.position));
        return spotDistance < range;
    }
}
