using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Halfwaypoint : MonoBehaviour {
	public GameObject FinishLap;
	public GameObject Halfway;

	void OnTriggerEnter(){
		FinishLap.SetActive (true);
		Halfway.SetActive (false);
	}
}
