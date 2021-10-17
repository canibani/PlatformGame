using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameObject bullet;
    [SerializeField] private int maxNoOfBullets;

    private Rigidbody rb;
    private bool isGrounded;
    private float movementX;
    private float movementY;
    private float rotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isGrounded = true;
    }

    void FixedUpdate() 
    {
        Move();
    }

    private void Move() 
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY).normalized;

        rb.AddRelativeForce(transform.forward + movement * speed);

        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, rotation, 0) * rotationSpeed * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
    
    void OnMove(InputValue movementValue)  
    { 
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnJump()
    {
        if(isGrounded) {
            isGrounded = false;
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    void OnLook(InputValue rotationValue)
    {
        Vector2 rotationVector = rotationValue.Get<Vector2>();

        rotation = rotationVector.x;
    }

    void OnFire() 
    {
        if (GameObject.FindGameObjectsWithTag("Bullet").Length < maxNoOfBullets) {
            Vector3 gun = transform.position + transform.forward * 2 + transform.up * 2;
            Instantiate(bullet, gun, transform.rotation);
        }
    }
    
    void OnCollisionStay()
    {
        isGrounded = true;
    }
}