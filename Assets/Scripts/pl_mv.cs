using UnityEngine;
using UnityEngine.InputSystem;

public class pl_mv : MonoBehaviour
{
    public float speed = 5.0f;

    private Rigidbody rb;
    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        ReadInput();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void ReadInput()
    {
        moveInput = new Vector2(
            Keyboard.current.aKey.isPressed ? -1f : Keyboard.current.dKey.isPressed ? 1f : 0f,
            Keyboard.current.sKey.isPressed ? -1f : Keyboard.current.wKey.isPressed ? 1f : 0f
        );
    }

    private void ApplyMovement()
    {
        Vector3 movement = transform.forward * moveInput.y + transform.right * moveInput.x;
        rb.velocity = movement * speed;
    }
}
