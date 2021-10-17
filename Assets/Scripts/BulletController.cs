using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 direction;
    private Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        direction = transform.forward;
        Destroy (gameObject, 10);
    }

    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    private void FixedUpdate() {        
        rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other) {
        Destroy(gameObject);
    }
}
