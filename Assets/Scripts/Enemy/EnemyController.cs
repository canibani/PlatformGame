using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private GameObject gravityInversionField;
    [SerializeField] private float targetLockDelay;

    private Rigidbody rb;
    private Transform player;
    private int lives;
    private float knockbackStrength;

    void Start()
    { 
        rb = GetComponent<Rigidbody>();
        lives = enemy.lives;
        knockbackStrength = 1000;
        player = GameObject.Find("Player").transform;
        StartCoroutine(Chasing());
    }
    private void Update() {
        if (lives <= 0) {
            Destroy(gameObject);
        }
    }
    
    private void Move() 
    {
        rb.AddForce(transform.forward * enemy.speed);
        
        transform.LookAt(player.transform);
    }


    private void CreateGravityInversionField() {
        GameObject gif = Instantiate(gravityInversionField, player.transform.position + transform.up * 5, Quaternion.identity);
    }

    IEnumerator Chasing() {
        while (Vector3.Distance(transform.position, player.transform.position) >= enemy.targetLockDistance) {
            Move();
            yield return null;
        }
        
        StartCoroutine(TargetLock());

    }

    IEnumerator TargetLock() {
        CreateGravityInversionField();

        yield return new WaitForSeconds(targetLockDelay);

        StartCoroutine(Chasing());
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Bullet") {
            Vector3 knockback = transform.position - other.transform.position;
            knockback.y = 0;
            rb.AddForce(knockback * knockbackStrength, ForceMode.Force);
            lives--;
        }
    }
}
