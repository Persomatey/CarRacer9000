/*
 * Programmer:   Hunter Goodin 
 * Date Created: 09/24/2018 @  9:40 PM 
 * Last Updated: 09/27/2018 @  1:30 PM by Hunter Goodin
 * File Name:    PlayerMovement.cs 
 * Description:  This script will be responsible for the player's core movement. 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody ballRB;        // To be populated with the ball's rigidbody in-engine 
    public Rigidbody carModel;      // To be populated with the car's art in-engine 
    public Transform playerTr;      // To be populated with the car's art in-engine... We're using it as a kind of compass to know where to apply force. 
    public float speed = 10;        // How fast the car can go. Set with an initial value, but can be changed in-engine. 
    public int howMuchCanTurn = 50; // How much we can allow the car to turn. Set with an initial value, but can be changed in-engine. 
    private int rotRightVeloc = 0;  // How fast the car is turning to the right. 
    private int rotLeftVeloc = 0;   // How fast the car is turning to the left. 
    public float slidyness = 2;     // How much the car maintains lateral momentum when sliding
    public float driftyness = 5;    // works similar to slidyness. driftyness should be greater than slidyness
    public float frictionSpeedThreshold = 5; // How fast before the car starts to slide
    private float velocOfRight;     // The velocity the car is moving to the right of the car 
    private float velocOfLeft;      // The velocity the car is moving to the left of the car 
    private int lastAccel = 0;      // The ylast direction the car was going in 
    private bool isW;               // We'll set this bool to if the W key being hit soon 
    private bool isA;               // We'll set this bool to if the A key being hit soon 
    private bool isS;               // We'll set this bool to if the S key being hit soon 
    private bool isD;               // We'll set this bool to if the D key being hit soon
    private bool isSpace;           // We'll set this bool if the Space(handbrake) is pressed

    public float debugVelocity;

    private void FixedUpdate()                                          // This function will be called every frame. 
    {
        velocOfRight = Vector3.Dot(ballRB.velocity, playerTr.right);    // Setting velocOfRight to whatever the velocity is to the right of the player 
        velocOfLeft = Vector3.Dot(ballRB.velocity, -playerTr.right);    // Setting velocOfLeft to whatever the velocity is to the left of the player 

        debugVelocity = ballRB.velocity.magnitude;

        if (Input.GetKey("w"))                                          // If the W key is being pressed... 
        { isW = true; }                                                 // Set isW to true 
        else                                                            // Otherwise... 
        { isW = false; }                                                // Set isW to false 
        if (Input.GetKey("a"))                                          // Do the same for the other keys 
        { isA = true; }
        else
        { isA = false; }
        if (Input.GetKey("s"))
        { isS = true; }
        else
        { isS = false; }
        if (Input.GetKey("d"))
        { isD = true; }
        else
        { isD = false; }
        if (Input.GetKey("space"))
        { isSpace = true; }
        else
        { isSpace = false; }

        if (isW == true && isA == false && isD == false)      // If only isW is true and nothing else (forward only), run this code 
        {
            rotRightVeloc--;                                            // Decrement rotRightVeloc 
            rotLeftVeloc--;                                             // Decrement rotLeftVeloc 
            lastAccel = 1;                                              // Set the lastAccel to 1 (this will come into play if no keys are being hit) 
            ballRB.AddForce(playerTr.forward * speed);                  // Move in the direction of the car's art (see, we're using the car's art as a sort of compass) 

            handleSliding(isSpace);
        }


        else if (isW == true && isA == true && isSpace == true)         //if (forward and left and handbrake), run handbraking drift turn
        {
            rotLeftVeloc++;                                             // Incriment rotLeftVeloc 
            rotRightVeloc--;                                            // Decrement rotRightVeloc 
            lastAccel = 3;                                              // Set the lastAccel to 3 (this will come into play if no keys are being hit) 
            carModel.transform.Rotate(Vector3.down * Time.deltaTime * rotLeftVeloc);    // Rotate out car's art to the left as fast as rotLeftVeloc's value 
            ballRB.AddForce(playerTr.forward * speed);                  // Move in the direction of the car's art (see, we're using the car's art as a sort of compass) 

            handleSliding(isSpace);
        }

        else if (isA == true && isSpace == true)         //if (left and handbrake), run handbraking drift turn
        {
            rotLeftVeloc--;                                             // Incriment rotLeftVeloc 
            rotRightVeloc--;                                            // Decrement rotRightVeloc 
            lastAccel = 3;                                              // Set the lastAccel to 3 (this will come into play if no keys are being hit) 
            carModel.transform.Rotate(Vector3.down * Time.deltaTime * rotLeftVeloc);    // Rotate out car's art to the left as fast as rotLeftVeloc's value 
            //ballRB.AddForce(playerTr.forward * speed);                  // Move in the direction of the car's art (see, we're using the car's art as a sort of compass) 

            handleSliding(isSpace);
        }

        else if (isW == true && isD == true && isSpace == true)         //if (forward and right and handbrake), run handbraking drift turn
        {
            rotRightVeloc++;                                            // Increment rotRightVeloc 
            rotLeftVeloc--;                                             // Decrement rotLeftVeloc 
            lastAccel = 4;                                              // Set the lastAccel to 4 (this will come into play if no keys are being hit) 
            carModel.transform.Rotate(Vector3.up * Time.deltaTime * rotRightVeloc);     // Rotate out car's art to the right as fast as rotRightVeloc's value 
            ballRB.AddForce(playerTr.forward * speed);                  // Move in the direction of the car's art (see, we're using the car's art as a sort of compass) 

            handleSliding(isSpace);
        }

        else if (isD == true && isSpace == true)         //if (right and handbrake), run handbraking drift turn
        {
            rotRightVeloc--;                                            // Increment rotRightVeloc 
            rotLeftVeloc--;                                             // Decrement rotLeftVeloc 
            lastAccel = 4;                                              // Set the lastAccel to 4 (this will come into play if no keys are being hit) 
            carModel.transform.Rotate(Vector3.up * Time.deltaTime * rotRightVeloc);     // Rotate out car's art to the right as fast as rotRightVeloc's value 
                                                                                        //ballRB.AddForce(playerTr.forward * speed);                  // Move in the direction of the car's art (see, we're using the car's art as a sort of compass) 

            handleSliding(isSpace);
        }

        else if ( isW == true && isA == true )                  // If isW is true and isA is true (forward and left), run this code 
        {
            rotLeftVeloc++;                                             // Incriment rotLeftVeloc 
            rotRightVeloc--;                                            // Decrement rotRightVeloc 
            lastAccel = 3;                                              // Set the lastAccel to 3 (this will come into play if no keys are being hit) 
            carModel.transform.Rotate(Vector3.down * Time.deltaTime * rotLeftVeloc);    // Rotate out car's art to the left as fast as rotLeftVeloc's value 
            ballRB.AddForce(playerTr.forward * speed);                  // Move in the direction of the car's art (see, we're using the car's art as a sort of compass) 

            handleSliding(isSpace);
        }
        else if ( isW == true && isD == true)                   // If isW is true and isD is true (forward and right), run this code 
        {
            rotRightVeloc++;                                            // Increment rotRightVeloc 
            rotLeftVeloc--;                                             // Decrement rotLeftVeloc 
            lastAccel = 4;                                              // Set the lastAccel to 4 (this will come into play if no keys are being hit) 
            carModel.transform.Rotate(Vector3.up * Time.deltaTime * rotRightVeloc);     // Rotate out car's art to the right as fast as rotRightVeloc's value 
            ballRB.AddForce(playerTr.forward * speed);                  // Move in the direction of the car's art (see, we're using the car's art as a sort of compass) 



            handleSliding(isSpace);
        }
        else if ( isS == true && isA == false && isD == false)  // If only isS is true and nothing else (backward only), run this code 
        {
            rotRightVeloc--;                                            // Decrement rotRightVeloc 
            rotLeftVeloc--;                                             // Decrement rotLeftVeloc 
            lastAccel = 2;                                              // Set the lastAccel to 2 (this will come into play if no keys are being hit) 
            ballRB.AddForce(-playerTr.forward * speed);                 // Move in the direction of the car's art (see, we're using the car's art as a sort of compass) 

            handleSliding(isSpace);
        }
        else if ( isS == true && isA == true)                    // If isS is true and isA is true (backward and left), run this code 
        {
            rotLeftVeloc++;                                             // Increment rotLeftVeloc 
            rotRightVeloc--;                                            // Decrement rotRightVeloc 
            lastAccel = 3;                                              // Set the lastAccel to 3 (this will come into play if no keys are being hit) 
            carModel.transform.Rotate(Vector3.down * Time.deltaTime * rotLeftVeloc);    // Rotate out car's art to the left as fast as rotLeftVeloc's value 
            ballRB.AddForce(-playerTr.forward * speed);                 // Move in the direction of the car's art (see, we're using the car's art as a sort of compass) 

            handleSliding(isSpace);
        }
        else if ( isS == true && isD == true)                   // If isS is true and isD is true (backward and Right), run this code 
        {
            rotRightVeloc++;                                            // Increment rotRightVeloc 
            rotLeftVeloc--;                                             // Decrement rotLeftVeloc 
            lastAccel = 4;                                              // Set the lastAccel to 4 (this will come into play if no keys are being hit) 
            carModel.transform.Rotate(Vector3.up * Time.deltaTime * rotRightVeloc);     // Rotate out car's art to the right as fast as rotRightVeloc's value 
            ballRB.AddForce(-playerTr.forward * speed);                 // Move in the direction of the car's art (see, we're using the car's art as a sort of compass) 

            handleSliding(isSpace);
        }
        else                                                    // Lastly, if there are no buttons being pressed, run this code 
        {
            rotRightVeloc--;                                            // Decrement rotRightVeloc 
            rotLeftVeloc--;                                             // Decrement rotLeftVeloc 

            /* These next two if statements are to make the car still turn a little in the direction it was just turning. The rotation will slow down as rotRightVeloc and rotLeftVeloc's value are decrementing every frame. This gives the car a realistic feeling. */ 

            if (lastAccel == 3 )                                        // if lastAccel is 3
            {
                carModel.transform.Rotate(Vector3.down * Time.deltaTime * rotLeftVeloc);    // rotate the car in the direction it was just being rotated as fast as rotLeftVeloc's value 
            }
            if (lastAccel == 4 )                                        // if lastAccel is 4 
            {
                carModel.transform.Rotate(Vector3.up * Time.deltaTime * rotRightVeloc);     // rotate the car in the direction it was just being rotated as fast as rotRightVeloc's value 
            }
            
            if (velocOfRight != 0)                                      // If the car is drifting to the right at all, 
            {
                ballRB.AddForce(-playerTr.right * velocOfRight);        // Apply a force to the left equal to that velocity 
            }
            if (velocOfLeft != 0)                                       // If the car is drifting to the left at all, 
            {
                ballRB.AddForce(playerTr.right * velocOfLeft);          // Apply a force to the right equal to that velocity 
            }
        }

        if (rotRightVeloc > howMuchCanTurn)     // Every frame, make sure that rotRightVeloc's value can't be more than howMuchCanTurn.  
        { rotRightVeloc = howMuchCanTurn; }     
        if (rotRightVeloc < 0)                  // Every frame, make sure that rotRightVeloc's value can't be less than 0. 
        { rotRightVeloc = 0; }
        if (rotLeftVeloc > howMuchCanTurn)      // Every frame, make sure that rotLeftVeloc's value can't be more than howMuchCanTurn.  
        {  rotLeftVeloc = howMuchCanTurn; }
        if (rotLeftVeloc < 0)                   // Every frame, make sure that rotLeftVeloc's value can't be less than 0. 
        { rotLeftVeloc = 0; }
    }

    public void handleSliding(bool handbrake)
    {
        if (!handbrake)                         // If the handbrake is not held down, the car will turn normally with normal slidyness
        {
            if (velocOfRight != 0)                                      // If the car is drifting to the right at all, 
            {
                if (ballRB.velocity.magnitude > frictionSpeedThreshold) // If the car is faster than friction threshold, then activate sliding
                {
                    ballRB.AddForce(-playerTr.right * velocOfRight / slidyness);
                }
                else
                {
                    ballRB.AddForce(-playerTr.right * velocOfRight);        // Apply a force to the left equal to that velocity 
                }
            }
            if (velocOfLeft != 0)                                       // If the car is drifting to the left at all, 
            {
                if (ballRB.velocity.magnitude > frictionSpeedThreshold) // If the car is faster than friction threshold, then activate sliding
                {
                    ballRB.AddForce(playerTr.right * velocOfLeft / slidyness);
                }
                else
                {
                    ballRB.AddForce(playerTr.right * velocOfLeft);        // Apply a force to the left equal to that velocity 
                }
            }
        }

        else                                               // if handbrake is held down
        {
            if (velocOfRight != 0)                                      // If the car is drifting to the right at all, 
            {     
                    ballRB.AddForce(-playerTr.right * velocOfRight / driftyness);
            }
            if (velocOfLeft != 0)                                       // If the car is drifting to the left at all, 
            {
                    ballRB.AddForce(playerTr.right * velocOfLeft / driftyness);
            }
        }
    }
}
