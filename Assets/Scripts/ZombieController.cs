using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour {

    public float speed = 1f;
    Animator anim;
    private Rigidbody rb;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 movement = new Vector3(0f, 0.0f, 0.0f);

        //rb.AddForce(movement * speed);
    }
}
