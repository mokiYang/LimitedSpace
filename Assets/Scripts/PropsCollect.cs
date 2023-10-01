using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsCollect : MonoBehaviour
{
    private bool playerNearby;
    private BagManager bagManager;

    private void Start()
    {
        bagManager = GameObject.FindWithTag("BagManager").GetComponent<BagManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }

    private void Update()
    {
        if (playerNearby)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("e");
            }
            
            if (Input.GetKeyDown(KeyCode.E) && (gameObject.tag == "Key" || gameObject.tag == "Potion" || gameObject.tag == "Art"))
            {
                PickupItem();
            }
            else if (Input.GetKeyDown(KeyCode.Space) && gameObject.tag == "Lock")
            {
                UseKey();
            }
            else if (Input.GetKeyDown(KeyCode.E) && gameObject.tag == "Map")
            {
                UseMap();
            }
        }
    }

    private void PickupItem()
    {
        bagManager.AddItem(gameObject.tag);
        Destroy(gameObject);
        bagManager.UpdateUI();
    }

    private void UseKey()
    {
        if (bagManager.UseItem("Key"))
        {
            bagManager.UseItem("Key");
            bagManager.UpdateUI();
        }
    }

    private void UseMap()
    {
        Destroy(gameObject);
        // TODO
    }
}
