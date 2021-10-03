using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private Transform player;
    [SerializeField] private float speed = 5;
    [SerializeField] private Vector3 rotationSpeed;

    private bool inRange;
    private Rigidbody rb;
    
    void Start()
    { 
        rb = GetComponent<Rigidbody>();
        inRange = false;
    }

    private void Update() {
        if (Vector3.Distance(transform.position, player.position) <= enemy.targetDistance) {
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
        if (inRange) {
            transform.position += transform.forward * speed * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.position - transform.position), 3 * Time.deltaTime);
        }
    }
}
