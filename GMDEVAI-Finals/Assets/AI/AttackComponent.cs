using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    private AIBase enemy;
    private GameObject player;

    [SerializeField] private float drainRate;
    [SerializeField] private float enemyDrainRange;
    [SerializeField] private float enemyDrainBonusRange;
    
    private IEnumerator drainMentalHealth;

    private bool drainingPlayer;
    public bool DrainingPlayer { get { return drainingPlayer; } }

    // Start is called before the first frame update
    void Start()
    {
        if(GetComponent<AIBase>() == null)
        {
            Debug.LogError("Invalid Component");
            return;
        }

        drainingPlayer = false;
        enemy = GetComponent<AIBase>();
        player = GetComponent<AIBase>().Player;

    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            return;

        float drainRange = enemyDrainRange;

        if(player.GetComponent<FirstPersonMovement>().IsRunning)
        {
            drainRange += enemyDrainBonusRange;
        }

        //Debug.Log("Enemy In Range:" + enemy.isInRange(enemyDrainRange));
        //Debug.Log("Can see Target: " + enemy.canSeeTarget());

        if(enemy.IsInRange(drainRange) && enemy.CanSeeTarget())
        {
            if (!drainingPlayer)
            {
                drainingPlayer = true;
                drainMentalHealth = player.GetComponent<MentalHealthComponent>().DecreaseMentalHealth(drainRate);
                StartCoroutine(drainMentalHealth);
            }
        }
        else
        {
            if (drainingPlayer)
            {
                drainingPlayer = false;
                StopCoroutine(drainMentalHealth);
                drainMentalHealth = null;
            }
        }
    }
}
