using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skidmarks : MonoBehaviour {

    public BoxCollider b;
    public GameObject skidmark;
    public int maxSkidmarks = 50;
    public float spawnRate = 1f;
    public bool isSliding;
    private bool spawning = false;
    public float height = .1f;
    public Vector3 sphere;
    public Vector3 frontLeft; // Top Left corner of the box collider
    public Vector3 frontRight; // Top Right corner of the box collider
    public Vector3 backLeft; // Bottom Left corner of the box collider
    public Vector3 backRight; // Bottom Right corner of the box collider
    GameObject thePlayer;

    private IEnumerator coroutine;


    void Start ()
    { 
        coroutine = Skid(spawnRate);
        thePlayer = GameObject.Find("Sphere");
    }

	void Update ()
    {
        frontLeft = transform.TransformPoint(b.center + new Vector3(-b.size.x, height*2, b.size.z) * 0.5f);
        frontRight = transform.TransformPoint(b.center + new Vector3(b.size.x, height * 2, b.size.z) * 0.5f);
        backLeft = transform.TransformPoint(b.center + new Vector3(-b.size.x, height * 2, -b.size.z) * 0.5f);
        backRight = transform.TransformPoint(b.center + new Vector3(b.size.x, height * 2, -b.size.z) * 0.5f);

        sphere = thePlayer.transform.position;

        isSliding = thePlayer.GetComponent<PlayerMovement>().getIsSliding();

        if (isSliding && !spawning)
        {
            StartCoroutine(coroutine);
        }
        if (!isSliding)
        {
            StopCoroutine(coroutine);
            spawning = false;               
        }

    }

    private IEnumerator Skid(float spawnRate)
    {
        while (true)
        {
            spawning = true;
            Instantiate(skidmark, frontLeft, Quaternion.identity);
            Instantiate(skidmark, frontRight, Quaternion.identity);
            Instantiate(skidmark, backLeft, Quaternion.identity);
            Instantiate(skidmark, backRight, Quaternion.identity);
            print("spawn");
            yield return new WaitForSeconds(1f/spawnRate);
        }
    }
}
