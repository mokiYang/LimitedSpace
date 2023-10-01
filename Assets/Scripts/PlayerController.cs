using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;

    private GameObject planet;
    private Rigidbody rb;
    private Vector3 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        planet = GameObject.FindWithTag("Plane");
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        moveDirection = (horizontal * transform.right + vertical * transform.forward).normalized;
    }

    void FixedUpdate()
    {
        // I cant describe in English hahah
        // 获取 player 真正的重力方向（指向球心
        Vector3 gravityUp = (rb.position - planet.transform.position).normalized;
        Vector3 localUp = transform.up;

        rb.AddForce(gravityUp * -9.81f);

        // Quaternion.FromToRotation(localUp, gravityUp) 计算从向上向量到重力方向的旋转
        // 这样就可以计算出 player 在球体表面运动时应该有的旋转
        Quaternion targetRotation = Quaternion.FromToRotation(localUp, gravityUp) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 50 * Time.fixedDeltaTime);

        Vector3 projectedMoveDirection = Vector3.ProjectOnPlane(moveDirection, gravityUp);
        rb.MovePosition(rb.position + projectedMoveDirection * speed * Time.fixedDeltaTime);

        // 让 player 保持在 plane 表面，不这么写 player 就会莫名向天上飞
        Vector3 newPosition = rb.position;
        newPosition = (newPosition - planet.transform.position).normalized * (planet.transform.localScale.x / 2);
        rb.MovePosition(planet.transform.position + newPosition);
    }
}
