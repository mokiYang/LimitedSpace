using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsCollect : MonoBehaviour
{
    public GameObject mapGuide;

    private bool playerNearby;
    private BagManager bagManager;

    void Start()
    {
        bagManager = GameObject.FindWithTag("BagManager").GetComponent<BagManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
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

    void Update()
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
            else if (Input.GetKeyDown(KeyCode.Space) && gameObject.tag == "Ring")
            {
                UseRing();
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
            bagManager.UpdateUI();
            gameObject.tag = "Art";
        }
    }


    private void UseMap()
    {
        Debug.Log("use map");
        CreateMapGuide();
        Destroy(gameObject);
    }

    private void UseRing()
    {
        // TODO
    }

    public void CreateMapGuide()
    {
        if (mapGuide != null)
        {
            Instantiate(mapGuide, transform.position, Quaternion.identity);
        }
    }
}
