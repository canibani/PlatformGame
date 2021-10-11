using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private Transform player;

    private bool chasing;
    private bool targetLock;
    private Rigidbody rb;
    
    void Start()
    { 
        rb = GetComponent<Rigidbody>();
        chasing = false;
        targetLock = false; 
    }

    private void Update() {
        CheckState();
    }

    private void FixedUpdate() 
    {
        Move();
        if (targetLock) {
            CreateGravityInversionField();
        }
    }

    private void Move() 
    {
        if (chasing && !targetLock) {
            rb.AddForce(transform.forward * enemy.speed);
        }
        transform.LookAt(player);
    }

    // This function should switch the state based on the enemy's distance from the player.
    // Chasing when the distance is less then maxChasingDistance
    // TargetLock when the distance is less then targetLockDistance
    private void CheckState() 
    {
        if (Vector3.Distance(transform.position, player.position) <= enemy.maxChasingDistance) {
            chasing = true;
        } else { 
            chasing = false;
        }
        if (Vector3.Distance(transform.position, player.position) <= enemy.targetLockDistance) {
            targetLock = true;
        } else {
            targetLock = false;
        }
    }

    private void CreateGravityInversionField() {
    }
}
