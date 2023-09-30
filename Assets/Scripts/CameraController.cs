using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Transform planet;
    public float distance = 15.0f;
    public float rotationSpeed = 5.0f;

    void Update()
    {
        Vector3 direction = (player.position - planet.position).normalized;

        Vector3 cameraPosition = player.position + direction * distance;

        transform.position = cameraPosition;

        Vector3 targetForward = (player.position - transform.position).normalized;

        Quaternion targetRotation = Quaternion.LookRotation(targetForward, transform.up);

        transform.rotation = targetRotation;
    }
}
