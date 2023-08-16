using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealComponent : MonoBehaviour
{
    private AIBase enemy;
    private GameObject player;

    [SerializeField] private float healRate;
    [SerializeField] private float healRange;

    private IEnumerator gainMentalHealth;

    private bool drainingPlayer;
    public bool DrainingPlayer { get { return drainingPlayer; } }

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<AIBase>() == null)
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

        float drainRange = healRange;

        if (enemy.IsInRange(drainRange) && enemy.CanSeeTarget())
        {
            if (!drainingPlayer)
            {
                drainingPlayer = true;
                gainMentalHealth = player.GetComponent<MentalHealthComponent>().IncreaseMentalHealth(healRate);
                StartCoroutine(gainMentalHealth);
            }
        }
        else
        {
            if (drainingPlayer)
            {
                drainingPlayer = false;
                StopCoroutine(gainMentalHealth);
                gainMentalHealth = null;
            }
        }
    }
}
