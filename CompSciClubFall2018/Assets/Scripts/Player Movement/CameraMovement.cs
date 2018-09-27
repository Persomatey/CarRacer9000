/*
 * Programmer:   Hunter Goodin 
 * Date Created: 09/24/2018 @  9:40 PM 
 * Last Updated: 09/27/2018 @  1:30 PM by Hunter Goodin
 * File Name:    CameraMovement.cs 
 * Description:  This script will be responsible for the camera's movement. 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;   // To be populated with the player in-engine 
    private Vector3 offset;     // The offset the camera will be from the player 

    void Start()
    {
        // offset is = to wherever the camera is on initialization subtracted by where the player is on initialization 
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        // move the camera to wherever the player is plus the offset 
        transform.position = player.transform.position + offset;
    }
}
