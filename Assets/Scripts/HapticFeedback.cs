
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HapticFeedback : MonoBehaviour
{
    // Start is called before the first frame update

    public XRBaseController controller;

    void OnTriggerEnter(Collider other) // WORDS
    {
        if (other.gameObject.tag == "cat")
        {
            controller.SendHapticImpulse((float)0.4, (float)0.4);
            UnityEngine.Debug.Log("Collided with cat");
        }
    }

}
