using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LifeManager : MonoBehaviour
{
    public bool isSafe = true;
    public float invisibleTime = 5.0f;
    public TextMeshProUGUI potionStatus;

    private LightCheck lightCheck;
    private BagManager bagManager;
    private bool inInvisible = false;

    void Start()
    {
        lightCheck = GameObject.FindWithTag("Player").GetComponent<LightCheck>();
        bagManager = GameObject.FindWithTag("BagManager").GetComponent<BagManager>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UsePotion();
        }

        isSafe = lightCheck.isInShadow || inInvisible;
        if (!isSafe)
        {
            // TODO: ÊØÎÀ×ß¹ýÀ´
        }
    }

    private void UsePotion()
    {
        if (bagManager.UseItem("Potion"))
        {
            bagManager.UpdateUI();
            inInvisible = true;
            string text = "Inbisible";
            potionStatus.text = text;
            Invoke("ResetInvisibility", invisibleTime);
        }
    }

    private void ResetInvisibility()
    {
        string text = "";
        potionStatus.text = text;
        inInvisible = false;
    }
}
