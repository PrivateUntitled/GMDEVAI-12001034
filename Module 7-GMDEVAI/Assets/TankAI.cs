using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAI : MonoBehaviour
{
    Animator animator;

    [SerializeField] private GameObject player;

    private ShootComponent shootComponent;
    private HPComponent hpComponent;

    public ShootComponent ShootComponent { get { return shootComponent; } }

    public GameObject Player { get { return player; } }

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        shootComponent = this.GetComponent<ShootComponent>();
        hpComponent = this.GetComponent<HPComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("distance", Vector3.Distance(transform.position, player.transform.position));
        animator.SetInteger("hp", hpComponent.Hp);
    }
}
