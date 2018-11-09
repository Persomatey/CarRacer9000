using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Halfwaypoint : MonoBehaviour
{
	public GameObject finishLap;
	public GameObject halfway;
    public bool checker = false;

    private void Update()
    {
        if(checker == true)
        {
            finishLap.SetActive(true);
            halfway.SetActive(false);
        }
        else if(checker == false)
        {
            finishLap.SetActive(false);
            halfway.SetActive(true);
        }
    }

    void OnTriggerEnter()
    {
        if(checker == false)
        {
            checker = true;
        }
        else if(checker == true)
        {
            checker = false;
        }
	}
}
