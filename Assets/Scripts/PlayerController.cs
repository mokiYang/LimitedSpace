using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float interactDistance = 2f;

    private GameObject planet;
    private BagManager bagManager;
    private Rigidbody rb;
    private Vector3 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        planet = GameObject.FindWithTag("Plane");
        bagManager = GameObject.FindWithTag("BagManager").GetComponent<BagManager>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        moveDirection = (horizontal * transform.right + vertical * transform.forward).normalized;

        if (Input.GetKeyDown(KeyCode.E))
        {
            PickupItem();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UseKey();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            bagManager.UseItem("Potion");
        }
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
        rb.MovePosition(rb.position + projectedMoveDirection * speed * Time.fixedDeltaTime);

        // �� player ������ plane ���棬����ôд player �ͻ�Ī�������Ϸ�
        Vector3 newPosition = rb.position;
        newPosition = (newPosition - planet.transform.position).normalized * (planet.transform.localScale.x / 2);
        rb.MovePosition(planet.transform.position + newPosition);
    }

    private void PickupItem()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactDistance))
        {
            if (hit.collider.CompareTag("Key") || hit.collider.CompareTag("Potion"))
            {
                bagManager.AddItem(hit.collider.tag);
                Destroy(hit.collider.gameObject);
                bagManager.UpdateUI();
            }
        }
    }

    private void UseKey()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactDistance))
        {
            if (hit.collider.CompareTag("Lock") && bagManager.UseItem("Key"))
            {
                bagManager.UseItem("Key");
                Debug.Log("use key");
                bagManager.UpdateUI();
            }
        }
    }
}
