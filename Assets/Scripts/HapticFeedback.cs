using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HapticFeedback : MonoBehaviour
{
    public XRBaseController controller;

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "cat") {
            controller.SendHapticImpulse((float)0.4, (float)0.4);
            UnityEngine.Debug.Log("Collided with cat");
        }
    }
    
}
