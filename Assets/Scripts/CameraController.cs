using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // 玩家的 Transform
    public Transform planet; // 行星的 Transform
    public float distance = 15.0f; // 摄像机距离玩家的距离

    void Update()
    {
        // 计算从行星到玩家的方向
        Vector3 direction = (player.position - planet.position).normalized;

        // 计算摄像机的位置
        Vector3 cameraPosition = player.position + direction * distance;

        // 将摄像机放置在计算出的位置
        transform.position = cameraPosition;

        // 计算实时的玩家向上向量
        Vector3 playerUp = (player.position - planet.position).normalized;

        // 使用实时的玩家向上向量计算摄像机应该具有的旋转
        Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position, playerUp);
        Debug.Log("targetRotation:" + targetRotation);

        // 应用旋转
        transform.rotation = targetRotation;
    }
}
