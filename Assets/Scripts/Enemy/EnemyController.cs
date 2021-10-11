using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private Transform player;
    [SerializeField] private float speed = 5;

    private bool inRange;
    private Rigidbody rb;
    
    void Start()
    { 
        rb = GetComponent<Rigidbody>();
        inRange = false;
    }

    private void Update() {
         if (Vector3.Distance(transform.position, player.position) <= enemy.maxChasingDistance) {
            inRange = true;
        } else {
            inRange = false;
        }
    }

    private void FixedUpdate() 
    {
        Move();
    }

    private void Move() {
        Vector3 direction = (player.transform.position - transform.position);
        rb.AddForce(transform.forward + direction * speed * Time.deltaTime);
        transform.LookAt(player);

    }

    // This function should switch the state based on the enemy's distance from the player.
    // Chasing when the distance is less then x
    // TargetLock when the distance is less then targetDistance
    /*private void CheckState() {


        if (Vector3.Distance(transform.position, player.position) <= enemy.targetDistance) {
            inRange = true;
        } else {
            inRange = false;
        }
    }*/
}
