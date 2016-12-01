using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public float distance = 2.0f;
    public float height = 0.2f;
    private Vector3 defaultDirection;
    

    // Use this for initialization
    void Start()
    {
        defaultDirection = new Vector3(1.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 direction = player.GetComponent<Rigidbody>().velocity.normalized;
        if(Mathf.Approximately(direction.x, 0) && Mathf.Approximately(direction.y, 0) && Mathf.Approximately(direction.z, 0))
        {
            direction = defaultDirection;
        }

        Vector3 position = player.transform.position - direction * distance;
        position.y = height;
        transform.position = position;
        transform.LookAt(player.transform);
    }
}
