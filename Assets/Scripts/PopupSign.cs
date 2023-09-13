using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupSign : MonoBehaviour
{
    public float delay = 3.0f; 
    Vector3 targetScale;
    public float animationDuration = 1.0f; 

    private Vector3 initialScale;
    private float startTime;

    void Start()
    {
        initialScale = transform.localScale;
        targetScale = initialScale * Random.Range(1f, 1.5f);

        startTime = Time.time;

        Invoke("DestroyObject", delay);
    }

    void Update()
    {
        float elapsed = Time.time - startTime;
        float lerpFactor = Mathf.Clamp01(elapsed / animationDuration);
        transform.localScale = Vector3.Lerp(initialScale, targetScale, lerpFactor);
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
