using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject player;
    [SerializeField] private TextMeshProUGUI ObjectiveText;

    [SerializeField] private GameObject[] BooksPrefab;
    [SerializeField] private List<GameObject> SpawnLocations;
    [SerializeField] private int spawnAmount;

    private List<GameObject> booksLeft = new List<GameObject>();

    [SerializeField] private int startingTime;
    private int timeRemaining;

    [SerializeField] private GameObject Goal;

    public GameObject Player { get { return player; } }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < spawnAmount; i++)
        {
            int randomSpawn = Random.Range(0, SpawnLocations.Count);

            GameObject bookToSpawn = Instantiate(BooksPrefab[Random.Range(0, BooksPrefab.Length)], 
                        SpawnLocations[randomSpawn].transform.position,
                        Quaternion.identity);

            SpawnLocations.RemoveAt(randomSpawn);

            booksLeft.Add(bookToSpawn);
        }

        ObjectiveText.text = "Books Left: " + booksLeft.Count;

        Goal.SetActive(false);

        timeRemaining = startingTime;
    }

    #region Book Collection Phase
    public void BookCollected(GameObject bookCollected)
    {
        player.GetComponentInChildren<ItemCollectionComponent>().DeselectBook(bookCollected);

        foreach (GameObject book in booksLeft)
        {
            if(bookCollected == book)
            {
                booksLeft.Remove(book);
                Destroy(book);
                break;
            }
        }
        
        if (NoBooksLeft())
        {
            StartCoroutine(this.StayAlive());
        }
        else
        {
            ObjectiveText.text = "Books Left: " + booksLeft.Count;
        }
    }

    bool NoBooksLeft()
    {
        return booksLeft.Count <= 0;
    }
    #endregion

    #region Staying Alive Phase
    IEnumerator StayAlive()
    {
        while (timeRemaining >= 0) 
        {
            ObjectiveText.text = "Time Left: " + timeRemaining;
            timeRemaining--;
            yield return new WaitForSeconds(1);
            yield return new WaitForSeconds(1);
        }

        Goal.SetActive(true);
    }
    #endregion
}