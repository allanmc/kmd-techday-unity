using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    const string PICKUP_TAG = "Pickup";
    const string ZOMBIE_TAG = "Zombie";
    const string ZOMBIE_MAIN_TAG = "ZombieMain";

    public float speed;
    public Text scoreText;
    public Text winText;
    public Camera camera;

    private Rigidbody rb;
    private int score;
    private int totalPickups;

    private List<Animator> zombieAnimators;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        score = 0;
        UpdateScoreText();
        winText.text = "";

        var getCount = GameObject.FindGameObjectsWithTag(PICKUP_TAG);
        totalPickups = getCount.Length;

        zombieAnimators = new List<Animator>();
        var zombies = GameObject.FindGameObjectsWithTag(ZOMBIE_MAIN_TAG);
        foreach (var zombie in zombies)
        {
            zombieAnimators.Add(zombie.GetComponent<Animator>());
        }
    }
	
	// Update is called once per frame
	void Update () {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(camera.transform.TransformDirection(movement) * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag) {
            case PICKUP_TAG:
                other.gameObject.SetActive(false);
                incrementScore();
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

    void incrementScore()
    {
        score++;
        UpdateScoreText();
        CheckWinConditions();
        DropZombies();
        RaiseZombies();
    }

    void DropZombies()
    {
        foreach(var animator in zombieAnimators)
        {
            animator.Play("back_fall");
        }
    }

    void RaiseZombies()
    {
        foreach (var animator in zombieAnimators)
        {
            animator.Play("walk");
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    void CheckWinConditions()
    {
        if (score >= totalPickups)
        {
            winText.text = "You is Winnar!";
            this.gameObject.SetActive(false);
        }
    }

    void LoseGame()
    {
        winText.text = "Ha Ha! You is Lose!";
        this.gameObject.SetActive(false);
    }

}
