using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HapticFeedback : MonoBehaviour
{
    //Creates a reference to the our actual controller
    public XRBaseController controller;
    void OnTriggerEnter(Collider other) //When our controller collides with another object
    {
        if (other.gameObject.tag == "cat") //If the other object has a tag of "cat"
        {
            controller.SendHapticImpulse((float)0.4, (float)0.4); //Send a haptic vibration of intensity 0.4 and length 0.4
            UnityEngine.Debug.Log("Collided with cat"); //Just to help us debug to make sure our controller and object collided
        }
    }
}