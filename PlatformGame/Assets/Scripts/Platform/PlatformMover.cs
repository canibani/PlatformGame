using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 target;
    private Rigidbody rb;
    private Vector3 currentTarget;
    private Vector3 startingPosition;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startingPosition = rb.position; 
        currentTarget = target;
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move() {
        transform.position = Vector3.MoveTowards(rb.position, currentTarget, speed * Time.deltaTime);

        if (rb.position == currentTarget) {
            Vector3 tempStartingPosition = startingPosition;
            Vector3 tempCurrentTarget = currentTarget;

            currentTarget = tempStartingPosition;
            startingPosition = tempCurrentTarget;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.transform.parent != null) {
            if (other.transform.parent.tag == "Player") {
                other.transform.parent.parent = transform;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.transform.parent != null) {
            if (other.transform.parent.tag == "Player") {
                other.transform.parent.parent = null;
            }
        }
    }
}
