using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Variables to store settings for the camera movement
    public Transform target;          // What the camera follows
    public float distance = 5.0f;    // How far the camera is from the target
    public float height = 2.0f;      // How high the camera is above the target
    public float smoothSpeed = 0.125f;  // How smoothly the camera moves
    public float rotationSpeed = 2.0f;  // How fast the camera can rotate
    public float minVerticalAngle = -30.0f;  // Minimum angle the camera can look down
    public float maxVerticalAngle = 60.0f;   // Maximum angle the camera can look up

    // Variables to keep track of the camera's rotation
    private float currentRotationX = 0.0f;  // Current horizontal rotation
    private float currentRotationY = 0.0f;  // Current vertical rotation

    // Offset determines the camera's initial position
    private Vector3 offset;

    // This code runs when the game starts
    private void Start()
    {
        // Set the camera's initial position based on height and distance
        offset = new Vector3(0, height, -distance);
    }

    // This code runs every frame, after all other updates
    private void LateUpdate()
    {
        // Read input from the mouse
        ReadInput();

        // Apply the camera's rotation
        ApplyRotation();

        // Apply the camera's position
        ApplyPosition();
    }

    // Read input from the mouse to control camera rotation
    private void ReadInput()
    {
        // Get how much the mouse has moved since the last frame
        float mouseX = Input.GetAxis("Mouse X") * 4.0f;
        float mouseY = Input.GetAxis("Mouse Y") * 4.0f;

        // Update the camera's horizontal and vertical rotation
        currentRotationX += mouseX * rotationSpeed;
        currentRotationY -= mouseY * rotationSpeed;

        // Make sure the camera doesn't look too far up or down
        currentRotationY = Mathf.Clamp(currentRotationY, minVerticalAngle, maxVerticalAngle);
    }

    // Apply the camera's rotation
    private void ApplyRotation()
    {
        // Make the camera look at the target
        transform.LookAt(target);

        // Rotate the target (what the camera is following) horizontally
        target.rotation = Quaternion.Euler(0, currentRotationX, 0);
    }

    // Apply the camera's position
    private void ApplyPosition()
    {
        // Create a rotation for the camera based on the current angles
        Quaternion rotation = Quaternion.Euler(currentRotationY, currentRotationX, 0);

        // Calculate where the camera should be based on the offset
        Vector3 desiredPosition = target.position + rotation * offset;

        // Smoothly move the camera to the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Update the camera's position
        transform.position = smoothedPosition;
    }
}
