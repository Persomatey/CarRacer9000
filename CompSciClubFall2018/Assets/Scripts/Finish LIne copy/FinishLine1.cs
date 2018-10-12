using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishLine1 : MonoBehaviour {
	public GameObject FinishLap;
	public GameObject Halfway;
	public GameObject LapCount;
	public int LapNum;

	void OnTriggerEnter(){
		LapNum++;
		LapCount.GetComponent<Text> ().text = "" + LapNum;
		FinishLap.SetActive (false);
		Halfway.SetActive (true);
	}
}
