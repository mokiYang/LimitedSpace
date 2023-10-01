using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCheck : MonoBehaviour
{
    public float checkDistance = 30f;

    private List<Light> pointLights;

    private void Start()
    {
        Light[] allLights = FindObjectsOfType<Light>();

        // only need SpotLight
        pointLights = new List<Light>();
        foreach (Light light in allLights)
        {
            if (light.type == LightType.Spot)
            {
                pointLights.Add(light);
            }
        }
    }

    private void Update()
    {
        Vector3 playerPosition = transform.position;

        bool isInShadow = true;

        foreach (Light pointLight in pointLights)
        {
            Vector3 lightPosition = pointLight.transform.position;
            float distanceToPlayer = Vector3.Distance(playerPosition, lightPosition);

            if (distanceToPlayer > checkDistance)
            {
                continue;
            }

            if (distanceToPlayer <= pointLight.range)
            {
                // buildings'shadow check
                Vector3 lightToPlayer = playerPosition - lightPosition;

                RaycastHit hit;
                if (Physics.Raycast(lightPosition, lightToPlayer.normalized, out hit, pointLight.range))
                {
                    if (hit.collider.gameObject == gameObject)
                    {
                        isInShadow = false;
                        break;
                    }
                }
            }
        }

        if (!isInShadow)
        {
            Debug.Log("in light");
        }
    }
}
