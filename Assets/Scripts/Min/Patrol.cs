using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyStates { PATROL, CHASE }
public class Patrol : MonoBehaviour
{
    private EnemyStates enemyState;
    public Vector3 startRotation;

    [Header("Basic Settings")]
    public float sightRadius;
    public float speed;
    private GameObject attackTarget;

    [Header("Patrol State")]
    public float patrolRange;
    private Vector3 wayPoint;
    private Vector3 rotateDir;
    Quaternion tempRot;
    float timer = 0f;

    private void Start()
    {
        enemyState = EnemyStates.PATROL;
        GetNewWayPoint();
    }

    private void Update()
    {
        SwitchStates();
    }

    void SwitchStates()
    {
        if (FoundPlayer())
        {
            enemyState = EnemyStates.CHASE;
        }

        switch (enemyState)
        {
            case EnemyStates.PATROL:
                if(timer/speed >= 0.99f)
                {
                    GetNewWayPoint();
                    timer = 0f;
                    print("aaa");
                }
                else
                {
                    transform.rotation = Quaternion.Lerp(tempRot, Quaternion.Euler(wayPoint), timer / speed);
                    timer += Time.deltaTime;
                    //transform.Rotate(rotateDir * speed * Time.deltaTime);
                }
                break;
            case EnemyStates.CHASE:
                if (!FoundPlayer())
                {
                    //backe to original position
                }
                else
                {
                    //rotate to target
                }
                break;
        }
        
    }

    bool FoundPlayer()
    {
        var colliders = Physics.OverlapSphere(transform.position, sightRadius);
        foreach (var item in colliders)
        {
            if (item.CompareTag("Player"))
            {
                attackTarget = item.gameObject;
                return true;
            }
        }
        attackTarget = null;
        return false;
    }

    void GetNewWayPoint()
    {
        float randomY = Random.Range(startRotation.y - patrolRange, startRotation.y + patrolRange);
        float randomZ = Random.Range(startRotation.z - patrolRange, startRotation.z + patrolRange);
        print(randomY);
        print(randomZ);

        Vector3 randomRotation = new Vector3(0, randomY, randomZ);
        wayPoint = randomRotation;
        tempRot = transform.rotation;
        //rotateDir = wayPoint - transform.rotation.eulerAngles;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRadius);
    }
}
