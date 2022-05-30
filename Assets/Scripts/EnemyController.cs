using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // keep it simple for now
    // we have basic random movement now

    [SerializeField] Vector3 randomDirection;
    [SerializeField] float timeToWander = Mathf.Infinity;
    float timeWandering = 0;
    [SerializeField] float speed;
    Rigidbody2D rb;

    
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {

        timeWandering += Time.deltaTime;

        if(timeWandering >= timeToWander)
        {
            // pick a direction
            randomDirection.x = Random.Range(-1,2);
            randomDirection.y = Random.Range(-1,2);

            // reset the timer
            timeWandering = 0;
        }
        // move in a direction
        Vector3 newPos = transform.position + randomDirection * Time.deltaTime;
        rb.MovePosition(newPos);
    }
    
}
