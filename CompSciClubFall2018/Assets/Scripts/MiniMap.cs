/*
 * Programmer: Brenden Plong 
 * Date created: 12/13/2018
 * Descripton: Allows the minimap camera to follow the postion of the player object
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour {


    public Transform player;

    void LateUpdate()
    {

        Vector3 newPosition = player.position; // Takes the player's position and stores it into a vector3 variable
        newPosition.y = transform.position.y; // Updates the camera's new position
        transform.position = newPosition; // The current position will be the player's current position

        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f); // Allows the camera to rotate along with the player
    }
}
