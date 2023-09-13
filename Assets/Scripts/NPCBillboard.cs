using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class NPCBillboard : MonoBehaviour
{
    private Transform mainCamera;

    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private Transform player;

    public GameObject canvas;
    public GameObject idleUI;
    public GameObject talkingUI;

    public bool talking = false;
    public bool talkingDone = true;

    public enum NPCState
    {
        NotInRange,
        Idle,
        Talking
    }

    private void Start()
    {
        mainCamera = Camera.main.transform;
        ChangeState(initialState);
    }

    public NPCState initialState = NPCState.NotInRange;
    private NPCState currentState;

    private InputAction interactAction;

    private void Awake()
    {
        // Set up the interact action
        interactAction = new InputAction(binding: "<Keyboard>/e");
        //interactAction.performed += ctx => OnInteract();
        interactAction.Enable();
    }

    private void LateUpdate()
    {
        transform.LookAt(mainCamera);
        transform.rotation = Quaternion.Euler(90f, transform.rotation.eulerAngles.y, 0f);
    }

    private void Update()
    {
        if (talking)
        {
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case NPCState.NotInRange:
                canvas.SetActive(false);
                if (distanceToPlayer <= detectionRadius)
                {
                    ChangeState(NPCState.Idle);
                }
                break;

            case NPCState.Idle:
                canvas.SetActive(true);
                if (distanceToPlayer > detectionRadius)
                {
                    ChangeState(NPCState.NotInRange);
                }
                if (interactAction.triggered)
                {
                    ChangeState(NPCState.Talking);
                }
                break;

            case NPCState.Talking:
                if (talkingDone)
                {
                    talkingDone = false;
                    talking = false;
                    talkingUI.SetActive(false);
                    ChangeState(NPCState.Idle);
                }
                else
                {
                    talking = true;
                    talkingUI.SetActive(true);
                    GetComponent<DialogueManager>().TextInit();
                }
                break;
        }
    }

    public void ChangeState(NPCState newState)
    {
        currentState = newState;
    }
}
