using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine1 : MonoBehaviour {
	public GameObject Lap;
	public GameObject Halfway;

	void OnTriggerEnter(){
		Lap.SetActive (true);
		Halfway.SetActive (false);
	}
}
