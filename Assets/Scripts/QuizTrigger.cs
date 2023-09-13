
//using System.Collections.Generic;
//using UnityEngine;
//using TMPro;

//public class QuizTrigger : MonoBehaviour
//{
//    private Transform mainCamera;
//    public Transform player;
//    public float detectionRadius = 2f;

//    public GameObject EButton;
//    //public GameObject quizPanel;

//    [SerializeField]
//    public List<GameObject> questions = new List<GameObject>();

//    int score = 0;
//    int curQues = 0;
//    public TextMeshProUGUI scoreText;
//    [SerializeField]
//    public GameObject scoreui;
//    bool quizStarted = false;
//    char correctAnswer;

//    void Start()
//    {
//        mainCamera = Camera.main.transform;
//        curQues = 0;
//        //ShowQuestion(curQues);
//    }

//    void Update()
//    {
//        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
//        if (distanceToPlayer <= detectionRadius)
//        {
//            EButton.SetActive(true);
//            if (Input.GetKeyDown(KeyCode.E))
//            {
//                //StartQuiz();
//                ShowQuestion(curQues);
//                quizStarted = true;
//            }
//        }
//        else
//        {
//            EButton.SetActive(false);
//        }
//        if (quizStarted)
//        {

//            switch (curQues)
//            {
//                case 0:
//                    correctAnswer = 'x';
//                    break;
//                case 1:
//                    correctAnswer = 'a';
//                    break;
//                case 2:
//                    correctAnswer = 'b';
//                    break;
//                case 3:
//                    correctAnswer = 'y';
//                    break;
//                case 4:
//                    correctAnswer = 'y';
//                    break;
//                default:
//                    return; // No more questions
//            }

//            if (Input.GetKeyDown(KeyCode.A) ||
//            Input.GetKeyDown(KeyCode.B) ||
//            Input.GetKeyDown(KeyCode.X) ||
//            Input.GetKeyDown(KeyCode.Y))
//            {
//                MoveToNextQuestion();
//            }
//            if (Input.GetKeyDown(KeyCode.A) && correctAnswer == 'a' ||
//           Input.GetKeyDown(KeyCode.B) && correctAnswer == 'b' ||
//           Input.GetKeyDown(KeyCode.X) && correctAnswer == 'x' ||
//           Input.GetKeyDown(KeyCode.Y) && correctAnswer == 'y')
//            {
//                score++;
//                UnityEngine.Debug.Log(correctAnswer + " pressed and is correct. Score: " + score);
//            }
//        }

//    }

//    private void LateUpdate()
//    {
//        transform.LookAt(mainCamera);
//        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + 90, 10f);
//    }

//    //void StartQuiz()
//    //{
//    //    quizPanel.SetActive(true);
//    //}

//    void ShowQuestion(int index)
//    {
//        questions[index].SetActive(true);
//    }

//    void MoveToNextQuestion()
//    {
//        questions[curQues].SetActive(false);
//        curQues++;

//        if (curQues < questions.Count)
//        {
//            ShowQuestion(curQues);
//        }
//        else
//        {
//            // Quiz finished, do something with the score
//            scoreText.text = score + "/5";
//            scoreui.SetActive(true);
//        }

//    }
//}
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Diagnostics;

public class QuizTrigger : MonoBehaviour
{
    private Transform mainCamera;
    public Transform player;
    public float detectionRadius = 2f;

    public GameObject EButton;
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
            EButton.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E) && !quizStarted)
            {
                StartQuiz();
                quizStarted = true;
            }
        }
        else
        {
            EButton.SetActive(false);
        }

        if (quizStarted)
        {
            if (Input.GetKeyDown(KeyCode.A) ||
                Input.GetKeyDown(KeyCode.B) ||
                Input.GetKeyDown(KeyCode.X) ||
                Input.GetKeyDown(KeyCode.Y))
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

        if (Input.GetKeyDown(KeyCode.A))
        {
            selectedAnswer = 'a';
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            selectedAnswer = 'b';
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            selectedAnswer = 'x';
        }
        else if (Input.GetKeyDown(KeyCode.Y))
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




