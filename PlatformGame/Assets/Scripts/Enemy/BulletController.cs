using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 direction;
    private Rigidbody rb;
    private float lifetime;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        direction = transform.forward;
        lifetime = 3;
        Destroy (gameObject, 10);
    }

    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
        Destroy(gameObject, lifetime);
    }

    private void FixedUpdate() {        
        rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other) {
        Destroy(gameObject);
    }
}
