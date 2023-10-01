using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TanZhaoDeng : MonoBehaviour
{
    Rigidbody rb;
    public Vector3 lightRotateSpeed;
    bool isForward = true;
    float timer;
    public float cd;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        timer = cd;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            isForward = !isForward;
            timer = cd;
        }

        if (isForward)
        {
            transform.Rotate(lightRotateSpeed * Time.deltaTime);
        }
        else if (!isForward)
        {
            transform.Rotate(-lightRotateSpeed * Time.deltaTime);
        }
        
    }
}
