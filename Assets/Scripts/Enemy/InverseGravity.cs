using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverseGravity : MonoBehaviour
{
    [SerializeField] private float launchSpeed;
    [SerializeField] private float delay;
    private Rigidbody player;

    private void Update() {
        Destroy(gameObject, delay);
    }
    
    private void FixedUpdate() {
        if (player != null) {
            player.AddForce(transform.up * launchSpeed);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.transform.parent != null) {
            if (other.transform.parent.tag == "Player") {
                player = other.transform.parent.GetComponent<Rigidbody>();
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.transform.parent != null) {
            if (other.transform.parent.tag == "Player") {
                player = null;
            }
        }
    }
}
