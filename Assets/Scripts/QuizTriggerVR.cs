
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Diagnostics;
using UnityEngine.InputSystem;

public class QuizTriggerVR : MonoBehaviour
{
    private Transform mainCamera;
    public Transform player;
    public float detectionRadius = 2f;

    [SerializeField]
    InputActionReference xbutton;
    [SerializeField]
    InputActionReference ybutton;
    [SerializeField]
    InputActionReference abutton;
    [SerializeField]
    InputActionReference bbutton;


    public GameObject interactUI;
    [SerializeField]
    public List<GameObject> questions = new List<GameObject>();

    int score = 0;
    int curQues = 0;
    public TextMeshProUGUI scoreText;
    [SerializeField]
    public GameObject scoreui;
    bool quizStarted = false;
    char correctAnswer;

    void Start()
    {
        mainCamera = Camera.main.transform;
        curQues = 0;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRadius)
        {
            interactUI.SetActive(true);
            if (abutton.action.triggered && !quizStarted)
            {
                StartQuiz();
                quizStarted = true;
            }
        }
        else
        {
            interactUI.SetActive(false);
        }

        if (quizStarted)
        {
            if (abutton.action.triggered ||
                bbutton.action.triggered ||
                xbutton.action.triggered ||
                ybutton.action.triggered)
            {
                CheckAnswer();
            }
        }
    }

    void LateUpdate()
    {
        transform.LookAt(mainCamera);
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + 90, 10f);
    }

    void StartQuiz()
    {
        ShowQuestion(curQues);
    }

    void ShowQuestion(int index)
    {
        questions[index].SetActive(true);
        DetermineCorrectAnswer();
    }

    void DetermineCorrectAnswer()
    {
        switch (curQues)
        {
            case 0:
                correctAnswer = 'x';
                break;
            case 1:
                correctAnswer = 'a';
                break;
            case 2:
                correctAnswer = 'b';
                break;
            case 3:
                correctAnswer = 'y';
                break;
            case 4:
                correctAnswer = 'y';
                break;
            default:
                break;
        }
    }

    void CheckAnswer()
    {
        char selectedAnswer = ' ';

        if (abutton.action.triggered)
        {
            selectedAnswer = 'a';
        }
        else if (bbutton.action.triggered)
        {
            selectedAnswer = 'b';
        }
        else if (xbutton.action.triggered)
        {
            selectedAnswer = 'x';
        }
        else if (ybutton.action.triggered)
        {
            selectedAnswer = 'y';
        }

        if (selectedAnswer == correctAnswer)
        {
            score++;
            UnityEngine.Debug.Log(selectedAnswer + " is correct. Score: " + score);
        }

        MoveToNextQuestion();
    }

    void MoveToNextQuestion()
    {
        questions[curQues].SetActive(false);
        curQues++;

        if (curQues < questions.Count)
        {
            ShowQuestion(curQues);
        }
        else
        {
            // Quiz finished, do something with the score
            scoreText.text = score + "/5";
            scoreui.SetActive(true);
        }

    }
}




