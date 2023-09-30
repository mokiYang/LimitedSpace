using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public Transform planet;

    private Rigidbody rb;
    private Vector3 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        moveDirection = (horizontal * transform.right + vertical * transform.forward).normalized;
    }

    void FixedUpdate()
    {
        // ��ȡ player ��������������ָ������
        Vector3 gravityUp = (rb.position - planet.position).normalized;
        // player ���ϵ�����
        Vector3 localUp = transform.up;

        rb.AddForce(gravityUp * -9.81f);

        // Quaternion.FromToRotation(localUp, gravityUp) ��������������������������ת
        // �����Ϳ��Լ���� player ����������˶�ʱӦ���е���ת
        Quaternion targetRotation = Quaternion.FromToRotation(localUp, gravityUp) * transform.rotation;
        // ��ֵ�� player ƽ����ת
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 50 * Time.fixedDeltaTime);

        Vector3 projectedMoveDirection = Vector3.ProjectOnPlane(moveDirection, gravityUp);
        rb.MovePosition(rb.position + projectedMoveDirection * speed * Time.fixedDeltaTime);

        // �� player ������ plane ���棬����ôд player �ͻ�Ī�������Ϸ�
        Vector3 newPosition = rb.position;
        newPosition = (newPosition - planet.position).normalized * (planet.localScale.x / 2);
        rb.MovePosition(planet.position + newPosition);
    }
}
