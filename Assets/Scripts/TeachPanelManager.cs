using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeachPanelManager : MonoBehaviour
{
    private bool isDetected = true;
    private StatusFollow statusFollow;

    private void Start()
    {
        statusFollow = GameObject.FindWithTag("StatusUI").GetComponent<StatusFollow>();
        statusFollow.CloseImage();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isDetected)
        {
            transform.GetComponent<CanvasGroup>().alpha = 0;
            statusFollow.ShowImage();
            isDetected = false;
        }
    }
}
