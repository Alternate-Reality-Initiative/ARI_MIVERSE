using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class TriggerGraffiti : MonoBehaviour
{
    public Transform player;
    public Camera camera1;
    public Camera camera2;
    public float detectionRadius = 1f;
    public GameObject paintButton;
    public float bounceScale = 1.2f;
    public float bounceDuration = 0.5f;
    public GameObject cursor;

    private Vector3 originalScale;
    private bool isAnimating = false;

    private InputAction interactAction;
    private InputAction mouseClickAction;

    private float initialPosX;

    public LayerMask playerLayer;

    public GameObject exitButton;

    private void Awake()
    {
        // Set up the input actions
        interactAction = new InputAction(binding: "<Keyboard>/e");
        interactAction.performed += ctx => Interact();

        mouseClickAction = new InputAction(binding: "<Mouse>/leftButton");
        mouseClickAction.performed += ctx => MouseClick();

        // Cache the original scale of the paintButton
        originalScale = paintButton.transform.localScale;

        // Enable the input actions
        interactAction.Enable();
        mouseClickAction.Enable();

        initialPosX = cursor.transform.position.x;
    }

    private void Update()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        //cursor.GetComponent<RectTransform>().position = mousePosition;
        Ray ray = RemoveBackground.Instance.currentCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        LayerMask layerMask = playerLayer;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            Debug.Log("hit");
            cursor.transform.position = new Vector3(initialPosX, hit.point.y, hit.point.z);
        }


        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRadius)
        {
            if (!RemoveBackground.Instance.graffitiMode)
            {
                paintButton.SetActive(true);
                exitButton.SetActive(false);
                if (!isAnimating)
                {
                    StartBounceAnimation();
                }
            }

            if (interactAction.triggered)
            {
                if (RemoveBackground.Instance.currentCamera == camera1)
                {
                    ToggleCameraSwitch();
                }
                else
                {
                    ToggleCameraSwitch();
                }
            }
        }
        else
        {
            paintButton.SetActive(false);
            isAnimating = false;
        }
    }

    private void ToggleCameraSwitch()
    {
        if (RemoveBackground.Instance.currentCamera == camera1)
        {
            paintButton.SetActive(false);
            exitButton.SetActive(true);

            SwitchCamera(camera2);
            player.gameObject.SetActive(false);
            RemoveBackground.Instance.graffitiMode = true;
            cursor.SetActive(true);
        }
        else
        {
            SwitchCamera(camera1);
            player.gameObject.SetActive(true);
            RemoveBackground.Instance.graffitiMode = false;
            isAnimating = false;
            cursor.SetActive(false);
        }
    }

    private void Interact()
    {
        ToggleCameraSwitch();
    }

    private void MouseClick()
    {
        if (RemoveBackground.Instance.graffitiMode)
        {
            // Handle mouse click logic here
        }
    }

    private void SwitchCamera(Camera newCamera)
    {
        if (RemoveBackground.Instance.currentCamera != null)
        {
            RemoveBackground.Instance.currentCamera.gameObject.SetActive(false);
        }

        RemoveBackground.Instance.currentCamera = newCamera;
        RemoveBackground.Instance.currentCamera.gameObject.SetActive(true);
    }

    private void StartBounceAnimation()
    {
        isAnimating = true;
        StartCoroutine(BounceAnimation());
    }

    private IEnumerator BounceAnimation()
    {
        Vector3 targetScale = originalScale * bounceScale;
        float startTime = Time.time;

        while (Time.time < startTime + bounceDuration)
        {
            float t = (Time.time - startTime) / bounceDuration;
            paintButton.transform.localScale = Vector3.Lerp(originalScale, targetScale, t);
            yield return null;
        }

        startTime = Time.time;

        while (Time.time < startTime + bounceDuration)
        {
            float t = (Time.time - startTime) / bounceDuration;
            paintButton.transform.localScale = Vector3.Lerp(targetScale, originalScale, t);
            yield return null;
        }

        paintButton.transform.localScale = originalScale;
        isAnimating = false;
    }
}
