using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Newtonsoft.Json;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DialogueManagerVR : MonoBehaviour
{
    public string dialogueFileName = "lucy";
    public float textSpeed;
    public float pauseLength;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Sprite dialogueSprite;
    public Image dialogueImage;

    private Dialogue[] dialogues;
    private int index = 0;
    private bool completeText = false;

    //private InputAction interactAction;
    [SerializeField]
    public InputActionReference abutton;

    private void Awake()
    {
        // Set up the interact action
        //interactAction = new InputAction(binding: "<Keyboard>/e");
        //interactAction.performed += ctx => OnInteract();
        //interactAction.Enable();

        abutton.action.performed += ctx => OnInteract();
        abutton.action.Enable();
    }

    private void Start()
    {
        dialogues = ParseDialogueFile(dialogueFileName);
        TextInit();
    }

    private void OnInteract()
    {
        if (GetComponent<NPCBillboardVR>().talking)
        {
            if (!completeText)
            {
                StopAllCoroutines();
                dialogueText.text = dialogues[index].dialogueText;
                completeText = true;
            }
            else
            {
                NextPage();
            }
        }
    }
    public Dialogue[] ParseDialogueFile(string dialogueFileName)
    {
        TextAsset dialogueFile = Resources.Load<TextAsset>("Dialogue/" + dialogueFileName);
        if (dialogueFile == null)
        {
            Debug.Log("json file not found");
            return null;
        }

        string jsonText = dialogueFile.text;
        Dialogue[] dialogues = JsonConvert.DeserializeObject<Dialogue[]>(jsonText);

        return dialogues;
    }

    public void TextInit()
    {
        dialogueImage.sprite = dialogueSprite;

        Debug.Log("text init called");

        Dialogue firstDialogue = dialogues[0];
        nameText.text = firstDialogue.speaker;
        dialogueText.text = "";

        StartCoroutine(TextFlow(firstDialogue.dialogueText));
    }

    IEnumerator TextFlow(string dialogue)
    {
        string str = "" + dialogue[0];

        for (int i = 1; i < dialogue.Length; ++i)
        {
            str += dialogue[i];
            if (dialogue[i-1] == '.' || dialogue[i-1] == '?' || dialogue[i-1] == '!')
            {
                yield return new WaitForSeconds(textSpeed + pauseLength);
            }
            else
            {
                yield return new WaitForSeconds(textSpeed);
            }
            dialogueText.text = str;
        }
        completeText = true;
    }

    public void NextPage()
    {
        UnityEngine.Debug.Log("inside page");
        if (dialogues.Length <= 1)
        {
            return;
        }
        index++;
        Debug.Log(index);
        StopAllCoroutines();

        completeText = false;

        if (index >= dialogues.Length)
        {
            GetComponent<NPCBillboardVR>().talking = false;
            GetComponent<NPCBillboardVR>().talkingDone = true;
            Debug.Log("last page");
            index = 0;
            return;
        }

        nameText.text = dialogues[index].speaker;

        StartCoroutine(TextFlow(dialogues[index].dialogueText));
    }
}
