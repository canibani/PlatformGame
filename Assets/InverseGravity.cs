using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverseGravity : MonoBehaviour
{
    private Rigidbody player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        if (player != null) {
            player.AddForce(transform.up);
        }
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.gameObject.GetComponentInParent());
        if (other.gameObject.tag == "Player") {
            player = other.gameObject.GetComponent<Rigidbody>();
        }
    }

    private void OnTriggerExit(Collider other) {

        if (other.gameObject.tag == "Player") {
            player = null;
        }
    }
}
