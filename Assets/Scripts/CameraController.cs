using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player;
    private GameObject planet;
    public float distance = 15.0f;
    public float rotationSpeed = 5.0f;

    void Start()
    {
        planet = GameObject.FindWithTag("Plane");
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        Vector3 direction = (player.transform.position - planet.transform.position).normalized;

        Vector3 cameraPosition = player.transform.position + direction * distance;

        transform.position = cameraPosition;

        Vector3 targetForward = (player.transform.position - transform.position).normalized;

        Quaternion targetRotation = Quaternion.LookRotation(targetForward, transform.up);

        transform.rotation = targetRotation;
    }
}
