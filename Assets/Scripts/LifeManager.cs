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
    private CanvasGroup successCanvas;
    private CanvasGroup failCanvas;
    private Coroutine countdownCoroutine;
    private StatusFollow statusFollow;

    void Start()
    {
        lightCheck = GameObject.FindWithTag("Player").GetComponent<LightCheck>();
        bagManager = GameObject.FindWithTag("BagManager").GetComponent<BagManager>();
        failCanvas = GameObject.FindWithTag("Fail").GetComponent<CanvasGroup>();
        statusFollow = GameObject.FindWithTag("StatusUI").GetComponent<StatusFollow>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UsePotion();
        }

        bool previousIsSafe = isSafe;
        isSafe = lightCheck.isInShadow || inInvisible;

        if (previousIsSafe && !isSafe)
        {
            if (countdownCoroutine != null)
            {
                StopCoroutine(countdownCoroutine);
            }
            countdownCoroutine = StartCoroutine(Countdown());
        }
        else if (!previousIsSafe && isSafe)
        {
            if (countdownCoroutine != null)
            {
                StopCoroutine(countdownCoroutine);
            }
        }

        if (lightCheck.isInShadow)
        {
            statusFollow.ChangeImage(3);
        }
        if (inInvisible)
        {
            statusFollow.ChangeImage(4);
        }
    }

    private void UsePotion()
    {
        if (bagManager.UseItem("Potion"))
        {
            bagManager.UpdateUI();
            inInvisible = true;
            string text = "Invisible";
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

    IEnumerator Countdown()
    {
        float countdownTime = 3.0f;

        while (countdownTime >= 0)
        {
            if (isSafe)
            {
                yield break;
            }

            if (countdownTime == 3)
            {
                statusFollow.ChangeImage(0);
            }
            else if (countdownTime == 2)
            {
                statusFollow.ChangeImage(1);
            }
            else if (countdownTime == 1)
            {
                statusFollow.ChangeImage(2);
            }

            yield return new WaitForSecondsRealtime(1);

            countdownTime --;
        }

        if (!isSafe)
        {
            // show fail
            failCanvas.alpha = 1;
            failCanvas.interactable = true;
            failCanvas.blocksRaycasts = true;
        }
    }
}
