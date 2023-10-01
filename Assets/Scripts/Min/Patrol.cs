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

    public float lookAtTime;
    float remainingTime;

    private void Start()
    {
        enemyState = EnemyStates.PATROL;
        GetNewWayPoint();
        remainingTime = lookAtTime;
    }

    private void Update()
    {
        //SwitchStates();
        if (timer / speed >= 0.99f)
        {
            if (remainingTime >= 0)
            {
                remainingTime -= Time.deltaTime;
            }
            else
            {
                GetNewWayPoint();
                remainingTime = lookAtTime;
                timer = 0f;
            }
        }
        else
        {
            transform.rotation = Quaternion.Lerp(tempRot, Quaternion.Euler(wayPoint), timer / speed);
            timer += Time.deltaTime;
            //transform.Rotate(rotateDir * speed * Time.deltaTime);
        }
    }

    void SwitchStates()
    {
        // if (FoundPlayer())
        // {
            // enemyState = EnemyStates.CHASE;
        // }

        switch (enemyState)
        {
            case EnemyStates.PATROL:
                if(timer/speed >= 0.99f)
                {
                    if(remainingTime >= 0)
                    {
                        remainingTime -= Time.deltaTime;
                    }
                    else
                    {
                        GetNewWayPoint();
                        remainingTime = lookAtTime;
                        timer = 0f;
                    }
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
                    enemyState = EnemyStates.PATROL;
                }
                else
                {
                    //rotate towards player
                    Vector3 directionToPlayer = attackTarget.transform.position - transform.position;
                    Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, speed);
                }
                break;
        }
        
    }

    bool FoundPlayer()
    {
        var colliders = Physics.OverlapSphere(transform.GetChild(0).position, sightRadius);
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
        float randomX = Random.Range(startRotation.x - patrolRange, startRotation.x + patrolRange);
        float randomY = Random.Range(startRotation.y - patrolRange, startRotation.y + patrolRange);
        float randomZ = Random.Range(startRotation.z - patrolRange, startRotation.z + patrolRange);

        Vector3 randomRotation = new Vector3(randomX, randomY, randomZ);
        wayPoint = randomRotation;
        tempRot = transform.rotation;
        //rotateDir = wayPoint - transform.rotation.eulerAngles;
    }

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.GetChild(0).position, sightRadius);
    //}
}
