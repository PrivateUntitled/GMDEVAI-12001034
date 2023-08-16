using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectionComponent : MonoBehaviour
{
    private GameObject bookInRadius;
    private bool collectingBook;

    private void Start()
    {
        collectingBook = false;
    }

    private void Update()
    {
        if(bookInRadius != null && Input.GetKey(KeyCode.Mouse0))
        {
            if (!collectingBook)
            {
                bookInRadius.GetComponent<CollectComponent>().PickupItem();
                collectingBook = true;
            }
        } 
        else
        {
            if (collectingBook && bookInRadius != null)
            {
                bookInRadius.GetComponent<CollectComponent>().CancelPickup();
                collectingBook = false;
            }
        }
    }

    public void DeselectBook(GameObject bookToDelete)
    {
        if(bookToDelete == bookInRadius)
        {
            bookInRadius = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<CollectComponent>() != null)
        {
            bookInRadius = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (collectingBook && bookInRadius != null)
        {
            bookInRadius.GetComponent<CollectComponent>().CancelPickup();
            collectingBook = false;
        }
    }
}
