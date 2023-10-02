using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeachPanelManager : MonoBehaviour
{
    private bool isDetected = true; 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isDetected)
        {
            transform.GetComponent<CanvasGroup>().alpha = 0;
            isDetected = false;
        }
    }
}
