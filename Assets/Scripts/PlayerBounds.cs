using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounds : MonoBehaviour
{
    public GameObject player;
    public GameObject warningPrefab;
    public LayerMask playerLayer;
    public Vector3 angleFace;

    public float maxSignHeight = 1f;

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("warni");

        if (collision.gameObject == player)
        { 
            Vector3 screenCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);
            Ray ray = Camera.main.ScreenPointToRay(screenCenter);

            RaycastHit hit;
            LayerMask layerMask = ~playerLayer;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                Vector3 position = new Vector3(hit.point.x, Random.Range(0.2f, maxSignHeight), hit.point.z);
                GameObject obj = Instantiate(warningPrefab, position, Quaternion.Euler(angleFace));

            }
        }
    }
}
