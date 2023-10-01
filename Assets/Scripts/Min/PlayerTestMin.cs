using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestMin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        print("gameover");
    }
}
