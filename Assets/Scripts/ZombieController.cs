using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour {

    public float maxSpeed = 0.05f;
    private float speed;
    Animator anim;
    private Rigidbody rb;
    public Terrain terrain;
    public Rigidbody player;

    private int terrainWidth; // terrain size (x)
    private int terrainLength; // terrain size (z)
    private int terrainPosX; // terrain position x
    private int terrainPosZ; // terrain position z
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        // terrain size x
        terrainWidth = (int)terrain.terrainData.size.x / 200;
        // terrain size z
        terrainLength = (int)terrain.terrainData.size.z / 200;
        // terrain x position
        terrainPosX = (int)terrain.transform.position.x;
        // terrain z position
        terrainPosZ = (int)terrain.transform.position.z;

        //Find random start location
        //rb.MovePosition(FindNewPositionNearPlayer());

        speed = Random.Range(0.01f, maxSpeed);
    }
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 playerPosition = player.position;
        playerPosition.y -= 0.5f;
        Vector3 targetDir = playerPosition - transform.position;
        Vector3 position = Vector3.MoveTowards(transform.position, playerPosition, speed);
        Vector3 rotation = Vector3.RotateTowards(transform.forward, targetDir, 0.1f, 0.0f);

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("walk"))
        {
            rb.MovePosition(position);
            rb.MoveRotation(Quaternion.LookRotation(rotation));
        }
    }

    Vector3 FindNewTerrainPosition()
    {
        int posx = Random.Range(terrainPosX, terrainPosX + terrainWidth);
        // generate random z position
        int posz = Random.Range(terrainPosZ, terrainPosZ + terrainLength);
        // get the terrain height at the random position
        float posy = Terrain.activeTerrain.SampleHeight(new Vector3(posx, 0, posz));

        return new Vector3(posx, posy, posz);
    }

    Vector3 FindNewPositionNearPlayer()
    {
        Vector3 startPos = Random.insideUnitSphere * 15f;
        startPos.x += startPos.x < 0 ? -5f : 5f;
        startPos.z += startPos.z < 0 ? -5f : 5f;

        // generate random z position
        Vector3 newPosition = new Vector3(player.position.x + startPos.x, 0.0f, player.position.z + startPos.z);

        return newPosition;
    }

}
