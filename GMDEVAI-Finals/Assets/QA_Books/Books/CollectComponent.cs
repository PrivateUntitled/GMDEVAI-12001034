using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectComponent : MonoBehaviour
{
    [SerializeField] private int timeToCollect; // in Seconds
    [SerializeField] private Image collectionBar;
    [SerializeField] private GameObject canvas;
    private int timeLeft = 0;

    private void Start()
    {
        collectionBar.fillAmount = (float)timeLeft / timeToCollect;
    }

    private void Update()
    {
        Transform playerRefTransform = GameManager.instance.Player.transform;

        canvas.transform.LookAt(GameManager.instance.Player.GetComponentInChildren<Camera>().transform);
    }

    public void PickupItem()
    {
        collectionBar.gameObject.SetActive(true);
        StartCoroutine("Pickup");
    }

    public void CancelPickup()
    {
        collectionBar.gameObject.SetActive(false);
        StopCoroutine("Pickup");
        timeLeft = 0;
    }

    IEnumerator Pickup()
    {
        while(timeToCollect >= timeLeft)
        {
            collectionBar.fillAmount = (float)timeLeft / timeToCollect;
            timeLeft++;
            yield return new WaitForSeconds(1);
        }

        GameManager.instance.BookCollected(this.gameObject);
    }
}
