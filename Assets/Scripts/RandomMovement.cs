using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public Collider movementBounds;
    public float swimSpeed = .1f;
    public float rotationSpeed = 1f;

    private Vector3 targetPosition;

    private void Start()
    {
        SetRandomTargetPosition();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, swimSpeed * Time.deltaTime);

        Quaternion targetRotation = Quaternion.LookRotation(-1 * (targetPosition - transform.position));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetRandomTargetPosition();
        }
    }

    private void SetRandomTargetPosition()
    {
        Vector3 boundsMin = movementBounds.bounds.min;
        Vector3 boundsMax = movementBounds.bounds.max;

        targetPosition = new Vector3(
            Random.Range(boundsMin.x, boundsMax.x),
            Random.Range(boundsMin.y, boundsMax.y),
            Random.Range(boundsMin.z, boundsMax.z)
        );
    }
}
