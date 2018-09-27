/*
 * Programmer:   Hunter Goodin 
 * Date Created: 09/24/2018 @  9:40 PM 
 * Last Updated: 09/27/2018 @  1:30 PM by Hunter Goodin
 * File Name:    ModelMovement.cs 
 * Description:  This script will be responsible for making sure the car's artwork is always following the player. 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarModelMovement : MonoBehaviour
{
    public GameObject player;   // To be populated with the player in-engine 
    private Vector3 offset;     // The offset the car will be from the player 

    void Start()
    {
        // offset is = to wherever the car's art is on initialization subtracted by where the player is on initialization 
        offset = transform.position - player.transform.position; 
    }

    void LateUpdate()
    {
        // move the car's art to wherever the player is plus the offset 
        transform.position = player.transform.position + offset; 
    }
}
