using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // ��ҵ� Transform
    public Transform planet; // ���ǵ� Transform
    public float distance = 15.0f; // �����������ҵľ���

    void Update()
    {
        // ��������ǵ���ҵķ���
        Vector3 direction = (player.position - planet.position).normalized;

        // �����������λ��
        Vector3 cameraPosition = player.position + direction * distance;

        // ������������ڼ������λ��
        transform.position = cameraPosition;

        // ����ʵʱ�������������
        Vector3 playerUp = (player.position - planet.position).normalized;

        // ʹ��ʵʱ����������������������Ӧ�þ��е���ת
        Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position, playerUp);
        Debug.Log("targetRotation:" + targetRotation);

        // Ӧ����ת
        transform.rotation = targetRotation;
    }
}
