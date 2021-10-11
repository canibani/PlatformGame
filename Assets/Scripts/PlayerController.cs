using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 rotationSpeed;
    private float jumpForce = 2f;
    private Vector3 jump;
    private bool isGrounded = true;
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private float rotationX;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
        isGrounded = true;

        rotationSpeed = new Vector3(0, 50, 0);
    }

    void FixedUpdate() 
    {
        Move();
    }

    private void Move() {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY).normalized;

        rb.AddRelativeForce(transform.forward + movement * speed);

        Quaternion deltaRotation = Quaternion.Euler(rotationSpeed * Time.deltaTime * rotationX);
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
    }
    
    void OnCollisionStay()
    {
        isGrounded = true;
    }
}