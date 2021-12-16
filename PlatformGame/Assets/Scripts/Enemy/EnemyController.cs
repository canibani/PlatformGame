using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private GameObject gravityInversionField;
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
        StartCoroutine(Wandering());
    }
    private void Update() {
        if (lives <= 0) {
            Destroy(gameObject);
        }
    }
    
    private void Chase() 
    {
        rb.AddForce(transform.forward * enemy.speed);

        Vector3 lookAtPlayer = player.transform.position;
        lookAtPlayer.y = transform.position.y;
        transform.LookAt(lookAtPlayer);
    }


    private void CreateGravityInversionField() {
        GameObject gif = Instantiate(gravityInversionField, player.transform.position + transform.up * 5, Quaternion.identity);
    }

    IEnumerator Wandering() {
        while (Vector3.Distance(transform.position, player.transform.position) >= enemy.maxChasingDistance) {
            // Do nothing, didn't want to implement the enemy wandering
            yield return null;
        }

        StartCoroutine(Chasing());
    }

    IEnumerator Chasing() {
        while (Vector3.Distance(transform.position, player.transform.position) >= enemy.targetLockDistance) {
            Chase();
            yield return null;
        }
        
        StartCoroutine(TargetLock());

    }

    IEnumerator TargetLock() {
        CreateGravityInversionField();

        yield return new WaitForSeconds(enemy.targetLockDelay);

        StartCoroutine(Wandering());
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
