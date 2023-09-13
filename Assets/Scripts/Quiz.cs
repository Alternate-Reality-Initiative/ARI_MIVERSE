//obsolete code but keeping just in case
////using System.Collections.Generic;
////using UnityEngine;
////using TMPro;

////public class Quiz : MonoBehaviour
////{
////    [SerializeField]
////    public List<GameObject> questions = new List<GameObject>();

////    int score = 0;
////    int curQues = 0;
////    public TextMeshProUGUI scoreText;

////    void Start()
////    {
////        curQues = 0;
////        ShowQuestion(curQues);
////    }

////    void Update()
////    {
////        if (Input.GetKeyDown(KeyCode.A) ||
////            Input.GetKeyDown(KeyCode.B) ||
////            Input.GetKeyDown(KeyCode.X) ||
////            Input.GetKeyDown(KeyCode.Y))
////        {
////            questions[curQues].SetActive(false);
////            curQues++;

////            if (curQues < questions.Count)
////            {
////                ShowQuestion(curQues);
////            }
////            else
////            {
////                // Quiz finished, do something with the score
////                textMeshPro.text = score +"/5";
////            }
////        }
////    }

////    void ShowQuestion(int index)
////    {
////        questions[index].SetActive(true);
////    }
////}



//using System.Collections.Generic;
//using System.Diagnostics;
//using UnityEngine;
//using TMPro;


//public class Quiz : MonoBehaviour
//{
//    [SerializeField]
//    public List<GameObject> questions = new List<GameObject>();

//    int score = 0;
//    int curQues = 0;

//    [SerializeField]
//    public TextMeshProUGUI scoreText;
//    [SerializeField]
//    public GameObject scoreui;

//    void Start()
//    {
//        curQues = 0;
//        ShowQuestion(curQues);
//    }

//    void Update()
//    {
//        char correctAnswer;

//        switch (curQues)
//        {
//            case 0:
//                correctAnswer = 'c';
//                break;
//            case 1:
//                correctAnswer = 'a';
//                break;
//            case 2:
//                correctAnswer = 'b';
//                break;
//            case 3:
//                correctAnswer = 'd';
//                break;
//            case 4:
//                correctAnswer = 'd';
//                break;
//            default:
//                return; // No more questions
//        }

//        if (Input.GetKeyDown(KeyCode.A) ||
//            Input.GetKeyDown(KeyCode.B) ||
//            Input.GetKeyDown(KeyCode.X) ||
//            Input.GetKeyDown(KeyCode.Y))
//        {
//            questions[curQues].SetActive(false);
//            curQues++;

//            if (curQues < questions.Count)
//            {
//                ShowQuestion(curQues);
//            }
//            else
//            {
//                scoreui.SetActive(true);
//                scoreText.text = score + "/5";
//            }
//        }

//        if (Input.GetKeyDown(KeyCode.A) && correctAnswer == 'a' ||
//            Input.GetKeyDown(KeyCode.B) && correctAnswer == 'b' ||
//            Input.GetKeyDown(KeyCode.X) && correctAnswer == 'x' ||
//            Input.GetKeyDown(KeyCode.Y) && correctAnswer == 'y')
//        {
//            score++;
//            UnityEngine.Debug.Log(correctAnswer + " pressed and is correct. Score: " + score);
//        }
//    }

//    void ShowQuestion(int index)
//    {
//        questions[index].SetActive(true);
//    }
//}
