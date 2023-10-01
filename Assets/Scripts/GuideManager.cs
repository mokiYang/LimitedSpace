using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideManager : MonoBehaviour
{
    public float guideTime = 2.0f;

    void Start()
    {
        Destroy(gameObject, guideTime);
    }
}
