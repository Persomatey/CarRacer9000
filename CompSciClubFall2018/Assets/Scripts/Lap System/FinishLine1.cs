using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishLine1 : MonoBehaviour
{
	public GameObject finishLap;
	public GameObject halfway;
	public GameObject lapCount;
	public int lapNum;
    public bool checker = false;    // F if the finish line is passed. T for if the halfway is passed. 

    private void Update()
    {
        if (checker == true)
        {
            finishLap.SetActive(false);
            halfway.SetActive(true);
        }
        else if (checker == false)
        {
            finishLap.SetActive(true);
            halfway.SetActive(false);
            lapCount.GetComponent<Text>().text = "" + lapNum;
        }
    }

    void OnTriggerEnter()
    {
        if (checker == false)
        {
            checker = true;
            lapNum++; 
        }
        else if (checker == true)
        {
            checker = false;
        }
    }
}
