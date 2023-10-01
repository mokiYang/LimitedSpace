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
        Quaternion rotationQuaternion = Quaternion.FromToRotation(transform.up, transform.right);
        transform.rotation = rotationQuaternion * transform.rotation;
        gameObject.tag = "Untagged";
        GameObject police = GameObject.FindWithTag("Police");
        Transform policeReal = police.transform.GetChild(0);
        if (police)
        {
            Quaternion targetRotation = Quaternion.FromToRotation(policeReal.transform.position, transform.position + new Vector3(1.5f, 1.5f, 1.5f));
            police.transform.rotation = Quaternion.Lerp(policeReal.transform.rotation, targetRotation, 5.0f);
        }
    }

    public void CreateMapGuide()
    {
        if (mapGuide != null)
        {
            Instantiate(mapGuide, transform.position, Quaternion.identity);
        }
    }
}
