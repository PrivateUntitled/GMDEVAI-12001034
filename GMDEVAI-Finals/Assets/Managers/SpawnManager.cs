using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField] private Spawn[] aIToSpawn;

    [Header("Spawn Areas")]
    [SerializeField] private GameObject[] spawnArea;
    
    [SerializeField] private GameObject player;

    [SerializeField] private GameObject enemyLocation;

    private List<GameObject> enemies = new List<GameObject>();

    public List<GameObject> Enemies { get { return enemies; } }

    private void Start()
    {
        for (int i = 0; i < aIToSpawn.Length; i++)
        {
            for (int j = 0; j < aIToSpawn[i].spawnAmount; j++)
            {
                GameObject randomSpawn = spawnArea[Random.Range(0, spawnArea.Length - 1)];
                GameObject aiToSpawn = Instantiate(aIToSpawn[i].aIType, randomSpawn.transform.position, Quaternion.identity);
                aiToSpawn.GetComponent<MovementComponent>().CurrentNode = randomSpawn;
                aiToSpawn.GetComponent<AIBase>().Init(player);
                aiToSpawn.transform.parent = enemyLocation.transform;
                if (aiToSpawn.GetComponent<AIBase>().AITypes != AITypes.FRIEND)
                {
                    enemies.Add(aiToSpawn);
                }
            }
        }
    }

    [System.Serializable]
    private struct Spawn
    {
        public GameObject aIType;
        public int spawnAmount;
    }
}