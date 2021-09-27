using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 20;
    [SerializeField] private float rotationSpeed = 20;

    [SerializeField] private float jumpForce = 2f;

    private Vector3 jump;
    private bool isGrounded = true;
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private float rotationX;
    private float rotationY;
    Vector3 m_EulerAngleVelocity;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
        isGrounded = true;

        //Set the angular velocity of the Rigidbody (rotating around the Y axis, 100 deg/sec)
        m_EulerAngleVelocity = new Vector3(0, 100, 0);
    }

    void FixedUpdate() 
    {
        Move();
        Rotate();

    }

    private void Move() {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY).normalized;
        transform.Translate(movement * speed * Time.deltaTime);
    }

    private void Rotate() 
    {
        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.fixedDeltaTime * rotationX);
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
        if(isGrounded){
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnLook(InputValue rotationValue)
    {
        Vector2 rotationVector = rotationValue.Get<Vector2>();

        rotationX = rotationVector.x; 
        rotationY = rotationVector.y;   
    }
    
    void OnCollisionStay()
    {
        isGrounded = true;
    }
}