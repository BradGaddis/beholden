using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public enum EnemyState{
    Patroling,
    Wandering,
    Idling,
    Attacking
}


public class EnemyController : MonoBehaviour
{
    // keep it simple for now
    // we have basic random movement now

    // EnemyState enemyState = EnemyState.Idling;
    [SerializeField] GameObject player;
    [SerializeField] Vector3 randomDirection;
    [SerializeField] float speed;
    [SerializeField] float timeToWander = Mathf.Infinity;
    [SerializeField] float wanderRadius = 5f;
    [SerializeField] float attackRadius = 1f;
    // [SerializeField] float attackTimer = Mathf.Infinity;
    float timeWandering = 0;
    // float attackTime = 0;
    Vector3 startPos;
    Rigidbody2D rb;
    
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    private void FixedUpdate()
    {
        // if(enemyState == EnemyState.Idling) // State TODO
        // {
        //     IdleBehaviour();
        // }
        
        // if(enemyState == EnemyState.Wandering) // State TODO
        // {
            // Wander(wanderRadius);    
        // }
        // chase the player

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if(distanceToPlayer <= wanderRadius)
        {
            if(distanceToPlayer <= attackRadius)
            {
                Attack();
            } else 
            {
                ChasePlayer();
            }
        }
        else {
            Wander(wanderRadius);
        }

    }

    private void Attack()
    {
        Debug.Log(this.gameObject.name + "is attacking");
    }

    private void ChasePlayer()
    {
        // the problem is here
        Vector2 currentPos = transform.position;
        Vector2 playerDirection = player.transform.position - transform.position;
        playerDirection = playerDirection.normalized;
        Vector2 newPos = (currentPos + playerDirection* speed * Time.deltaTime);
        rb.MovePosition(newPos);
    }

    private void IdleBehaviour()
    {
        // TODO
    }

    private void Wander(float radius = 1f)
    {
        timeWandering += Time.deltaTime;

        if (timeWandering >= timeToWander)
        {
            // pick a direction
            randomDirection.x = Random.Range(-1, 2);
            randomDirection.y = Random.Range(-1, 2);

            // reset the timer
            timeWandering = 0;
        }

        // check distance against starting position
        float distanceFromStartPos = Vector2.Distance(transform.position, startPos);

        if (distanceFromStartPos >= radius)
        {
            ReturnHome();
        }
        // move in a direction
        
        WalkRandomDirection();
    }

    private void ReturnHome()
    {
    }

    private void WalkRandomDirection()
    {
        Vector3 newPos = transform.position + randomDirection * Time.deltaTime;
        rb.MovePosition(newPos);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(startPos, wanderRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,wanderRadius);
    }
}
