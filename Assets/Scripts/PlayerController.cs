using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float lightSpeed = 3.0f;
    public float interactDistance = 2f;

    private LightCheck lightCheck;
    private GameObject planet;
    private Rigidbody rb;
    private Vector3 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        planet = GameObject.FindWithTag("Plane");
        lightCheck = GameObject.FindWithTag("Player").GetComponent<LightCheck>();
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
        Vector3 gravityUp = (rb.position - planet.transform.position).normalized;
        Vector3 localUp = transform.up;

        rb.AddForce(gravityUp * -9.81f);

        // Quaternion.FromToRotation(localUp, gravityUp) ��������������������������ת
        // �����Ϳ��Լ���� player ����������˶�ʱӦ���е���ת
        Quaternion targetRotation = Quaternion.FromToRotation(localUp, gravityUp) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 50 * Time.fixedDeltaTime);

        Vector3 projectedMoveDirection = Vector3.ProjectOnPlane(moveDirection, gravityUp);
        float realSpeed = lightCheck.isInShadow ? speed : lightSpeed;
        rb.MovePosition(rb.position + projectedMoveDirection * realSpeed * Time.fixedDeltaTime);

        // �� player ������ plane ���棬����ôд player �ͻ�Ī�������Ϸ�
        Vector3 newPosition = rb.position;
        newPosition = (newPosition - planet.transform.position).normalized * (planet.transform.localScale.x / 2);
        rb.MovePosition(planet.transform.position + newPosition);
    }
}
