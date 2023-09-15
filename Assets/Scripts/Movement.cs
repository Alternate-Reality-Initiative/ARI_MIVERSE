using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Create a movement vector based on input.
        Vector3 direction = transform.forward * verticalInput + transform.right * horizontalInput;

        // Apply the speed to the movement vector.
        Vector3 velocity = direction.normalized * speed * Time.deltaTime;

        // Set the velocity of the Rigidbody.
        rb.velocity = velocity;
    }
}