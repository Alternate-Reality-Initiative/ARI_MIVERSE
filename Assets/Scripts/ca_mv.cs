using UnityEngine;
using UnityEngine.InputSystem;

public class ca_mv : MonoBehaviour
{
    public Transform target;
    public float distance = 5.0f;
    public float height = 2.0f;
    public float smoothSpeed = 0.125f;
    public float rotationSpeed = 2.0f;
    public float minVerticalAngle = -30.0f;
    public float maxVerticalAngle = 60.0f;

    private float currentRotationX = 0.0f;
    private float currentRotationY = 0.0f;
    private Vector3 offset;

    private void Start()
    {
        offset = new Vector3(0, height, -distance);
    }

    private void LateUpdate()
    {
        ReadInput();
        ApplyRotation();
        ApplyPosition();
    }

    private void ReadInput()
    {
        Vector2 rotationInput = Mouse.current.delta.ReadValue() * rotationSpeed;
        currentRotationX += rotationInput.x;
        currentRotationY -= rotationInput.y;
        currentRotationY = Mathf.Clamp(currentRotationY, minVerticalAngle, maxVerticalAngle);
    }

    private void ApplyRotation()
    {
        Quaternion rotation = Quaternion.Euler(currentRotationY, currentRotationX, 0);
        transform.LookAt(target);
        target.rotation = Quaternion.Euler(0, currentRotationX, 0);
    }

    private void ApplyPosition()
    {
        Quaternion rotation = Quaternion.Euler(currentRotationY, currentRotationX, 0);
        Vector3 desiredPosition = target.position + rotation * offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
