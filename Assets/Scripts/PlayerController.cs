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
        // 获取 player 真正的重力方向（指向球心
        Vector3 gravityUp = (rb.position - planet.position).normalized;
        // player 向上的向量
        Vector3 localUp = transform.up;

        rb.AddForce(gravityUp * -9.81f);

        // Quaternion.FromToRotation(localUp, gravityUp) 计算从向上向量到重力方向的旋转
        // 这样就可以计算出 player 在球体表面运动时应该有的旋转
        Quaternion targetRotation = Quaternion.FromToRotation(localUp, gravityUp) * transform.rotation;
        // 插值让 player 平滑旋转
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 50 * Time.fixedDeltaTime);

        Vector3 projectedMoveDirection = Vector3.ProjectOnPlane(moveDirection, gravityUp);
        rb.MovePosition(rb.position + projectedMoveDirection * speed * Time.fixedDeltaTime);

        // 让 player 保持在 plane 表面，不这么写 player 就会莫名向天上飞
        Vector3 newPosition = rb.position;
        newPosition = (newPosition - planet.position).normalized * (planet.localScale.x / 2);
        rb.MovePosition(planet.position + newPosition);
    }
}
