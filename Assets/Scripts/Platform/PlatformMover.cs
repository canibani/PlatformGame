using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 target;
    private Rigidbody rb;
    private Vector3 currentDirection;
    private Vector3 currentTarget;
    private Vector3 startingPosition;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startingPosition = rb.position; 
        currentDirection = target - rb.position;
        currentTarget = target;
    }

    void FixedUpdate()
    {
        Move();     
    }

    private void Move() {
        // I would like to change this at some point such that given a number for speed, all platforms move at the same speed.
        // Currently it moves quicker, the further the distance it travels.
        // This solution gives me the ability to create diagonally moving platforms.
        rb.MovePosition(rb.position + speed * currentDirection * Time.deltaTime);
        if (rb.position == currentTarget) {
            currentDirection = currentDirection * -1;
            currentTarget = rb.position == target ? startingPosition: target;
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
