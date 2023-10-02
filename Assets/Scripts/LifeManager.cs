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
    private CanvasGroup countdown1;
    private CanvasGroup countdown2;
    private CanvasGroup countdown3;
    private CanvasGroup invisible;
    private CanvasGroup hide;
    private Coroutine countdownCoroutine;

    void Start()
    {
        lightCheck = GameObject.FindWithTag("Player").GetComponent<LightCheck>();
        bagManager = GameObject.FindWithTag("BagManager").GetComponent<BagManager>();
        failCanvas = GameObject.FindWithTag("Fail").GetComponent<CanvasGroup>();
        countdown1 = GameObject.FindWithTag("1").GetComponent<CanvasGroup>();
        countdown2 = GameObject.FindWithTag("2").GetComponent<CanvasGroup>();
        countdown3 = GameObject.FindWithTag("3").GetComponent<CanvasGroup>();
        invisible = GameObject.FindWithTag("Invisible").GetComponent<CanvasGroup>();
        hide = GameObject.FindWithTag("Hide").GetComponent<CanvasGroup>();
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

        if (isSafe)
        {
            countdown1.alpha = 0;
            countdown2.alpha = 0;
            countdown3.alpha = 0;
        }
        hide.alpha = lightCheck.isInShadow ? 1 : 0;
        invisible.alpha = inInvisible ? 1 : 0;
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
                countdown1.alpha = 1;
                countdown2.alpha = 0;
                countdown3.alpha = 0;
            }
            else if (countdownTime == 2)
            {
                countdown1.alpha = 0;
                countdown2.alpha = 1;
                countdown3.alpha = 0;
            }
            else if (countdownTime == 1)
            {
                countdown1.alpha = 0;
                countdown2.alpha = 0;
                countdown3.alpha = 1;
            }

            yield return new WaitForSecondsRealtime(1);

            countdownTime --;
        }

        if (!isSafe)
        {
            countdown1.alpha = 0;
            countdown2.alpha = 0;
            countdown3.alpha = 0;
            // show fail
            failCanvas.alpha = 1;
            failCanvas.interactable = true;
            failCanvas.blocksRaycasts = true;
        }
    }
}
