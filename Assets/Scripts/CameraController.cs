using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    [SerializeField] private float cameraDistance = 15f;
    [SerializeField] private float cameraHeight = 10f;

    // Start is called before the first frame update
    void Start()
    {
    }
    
    private void LateUpdate() {
        transform.position = player.transform.position - player.transform.forward * cameraDistance;
        transform.LookAt(player.transform.position);
        transform.position = new Vector3 (transform.position.x, transform.position.y + cameraHeight, transform.position.z);
    }
}
