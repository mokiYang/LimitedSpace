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
                if(Vector3.Distance(wayPoint, transform.rotation.eulerAngles) <= 1)
                {
                    GetNewWayPoint();
                }
                else
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(wayPoint), speed * Time.deltaTime);
                    print(wayPoint.y);
                    //rotate to waypoint;
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

        Vector3 randomRotation = new Vector3(0, randomY, randomZ);
        wayPoint = randomRotation;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRadius);
    }
}
