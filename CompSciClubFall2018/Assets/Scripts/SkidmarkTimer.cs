using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkidmarkTimer : MonoBehaviour {

    public float timer = 5f;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, timer);
	}
}
