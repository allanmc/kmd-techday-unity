using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    const string PICKUP_TAG = "Pickup";
    const string ZOMBIE_TAG = "Zombie";

    public float speed;
    public Text scoreText;
    public Text winText;

    private Rigidbody rb;
    private int score;
    private int totalPickups;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        score = 0;
        UpdateScore();

        var getCount = GameObject.FindGameObjectsWithTag(PICKUP_TAG);
        totalPickups = getCount.Length;

        winText.text = "";
    }
	
	// Update is called once per frame
	void Update () {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag) {
            case PICKUP_TAG:
                other.gameObject.SetActive(false);
                score++;
                UpdateScore();
                CheckWinConditions();
                break;
            case ZOMBIE_TAG:
                LoseGame();
                break;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnTriggerEnter(collision.collider);
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    void CheckWinConditions()
    {
        if (score >= totalPickups)
        {
            winText.text = "You is Winnar!";
        }
    }

    void LoseGame()
    {
        winText.text = "Ha Ha! You is Losar!";
    }
}
