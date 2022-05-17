using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuizController : MonoBehaviour
{

    public static QuizController instance;
    public QuizUIScript uIScript;

    public bool isReady { get; private set; }
    public bool isQuestionVisible;
    public bool isAnswerGiven;

    public int CurrentQuestion;
    public string CurrentAnswer;

    public List<QuizClass.QuestionsClass> ServerQuestionsList = new List<QuizClass.QuestionsClass>();

    public List<QuizClass.QuestionsClass> SavedAnswerdList = new List<QuizClass.QuestionsClass>();


    public float QuizStartTime;
    public float QuizEndTime;


    public GameObject answerPrefab;
    public GameObject Lane_Left, Lane_Middle, Lane_Right, zPos;

    private void Awake()
    {
        instance = this;
    }
    IEnumerator Start()
    {
        yield return new WaitUntil(() => APIController.instance.isReady);
        if (APIController.instance.serverStatus == APIController.ServerStatus.Online)
        {
            startFetchQuestion();
        }
        else
        {
            //StartShowQuestion();
        }
    }

    public void startFetchQuestion()
    {
        StartCoroutine(getAllQuestions());
    }

    private IEnumerator getAllQuestions()
    {
        ServerQuestionsList.Clear();
        SavedAnswerdList.Clear();

        yield return new WaitUntil(() => PlayerDataController.instance.isReady);

        StartCoroutine(APIController.instance.GetQuestions(PlayerDataController.instance.GradeCount, PlayerDataController.instance.LevelCount, (status) =>
        {
            if (status != null && !string.IsNullOrEmpty(status.ToString()))
            {
                string data = null;
                data = status;
                // Debug.Log(data);
                StartCoroutine(saveJsonData(data));
            }

        }));
    }

    private IEnumerator saveJsonData(string str)
    {
        if (!string.IsNullOrEmpty(str))
        {
            // Debug.Log(str);
            var jsonData = JObject.Parse(str);
            JToken questionArray = jsonData["Items"];

            // JArray questionArray = JArray.Parse(str);
            int count = questionArray.Count();
            // Debug.Log(count);
            if (count > 5)
            {
                for (int i = 0; i < count; i++)
                {
                    QuizClass.QuestionsClass questionsClass = new QuizClass.QuestionsClass();
                    questionsClass.QuestionId = questionArray[i]["id"].ToString();
                    questionsClass.Question = questionArray[i]["question"].ToString();
                    questionsClass.optionA = questionArray[i]["option1"].ToString();
                    questionsClass.optionB = questionArray[i]["option2"].ToString();
                    questionsClass.optionC = questionArray[i]["option3"].ToString();
                    questionsClass.answer = questionArray[i]["correctAnswer"].ToString();
                    ServerQuestionsList.Add(questionsClass);
                    if (ServerQuestionsList.Count == 6)
                    {
                        break;
                    }
                    // Debug.Log(questionsClass);

                }
                isReady = true;
                yield return new WaitUntil(() => isReady);
                StartShowQuestion();
            }
        }
        else
        {
            Debug.Log("No Question On Server");
        }
        yield return null;
    }

    private void StartShowQuestion()
    {
        StartCoroutine(ShowQuestion(ServerQuestionsList[CurrentQuestion]));
    }

    private IEnumerator ShowQuestion(QuizClass.QuestionsClass questionsClass)
    {
        yield return new WaitUntil(() => GameController.instance.isGameStart && !GameController.instance.isPlayerDead);
        yield return new WaitUntil(() => isQuestionVisible == false);
        yield return new WaitForSeconds(15f);
        isQuestionVisible = true;
        yield return new WaitForSeconds(8f);
        if (GameController.instance.isPlayerDead)
        {

            yield return new WaitUntil(() => GameController.instance.isGameStart && !GameController.instance.isPlayerDead);
            yield return new WaitForSeconds(2f);

        }
        CurrentQuestion = ServerQuestionsList.IndexOf(questionsClass);
        CurrentAnswer = ServerQuestionsList[CurrentQuestion].answer;
        SavedAnswerdList.Add(ServerQuestionsList[CurrentQuestion]);
        uIScript.setQuizQuestion(questionsClass.Question, questionsClass.optionA, questionsClass.optionB, questionsClass.optionC);

        yield return new WaitForSeconds(16f);
        if (PowerUPController.instance.maxPowerInUse == 0)
        {
            ScreenController.instance.PowerPurchaseAtStart.GetComponent<PowerPurchaseAtStart>().setPower(Save.sloMoPower, true, 0);
            yield return new WaitForSeconds(1f);
            ScreenController.instance.PowerPurchaseAtStart.SetActive(true);
        }
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(spawnAwswerObject());

        yield return new WaitForSeconds(5f);


        yield return new WaitUntil(() => isAnswerGiven);

        if (ServerQuestionsList.Count > CurrentQuestion + 1)
        {

            StartCoroutine(ShowQuestion(ServerQuestionsList[CurrentQuestion + 1]));
        }
        else
        {
            yield return new WaitForSeconds(2f);
            checkForQuestionCompletion();
        }
        yield return new WaitForSeconds(2f);
        isQuestionVisible = false;
        isAnswerGiven = false;
    }


    private IEnumerator spawnAwswerObject()
    {
        GameObject go1 = Instantiate(answerPrefab, new Vector3(Lane_Left.transform.position.x, answerPrefab.transform.position.y, zPos.transform.position.z), answerPrefab.transform.rotation);
        go1.GetComponent<QuizAnswerObjectScript>().setText("A");
        GameObject go2 = Instantiate(answerPrefab, new Vector3(Lane_Middle.transform.position.x, answerPrefab.transform.position.y, zPos.transform.position.z), answerPrefab.transform.rotation);

        go2.GetComponent<QuizAnswerObjectScript>().setText("B");
        GameObject go3 = Instantiate(answerPrefab, new Vector3(Lane_Right.transform.position.x, answerPrefab.transform.position.y, zPos.transform.position.z), answerPrefab.transform.rotation);
        go3.GetComponent<QuizAnswerObjectScript>().setText("C");

        yield return null;


    }

    public bool checkCorrectAnswer(string s)
    {


        if (CurrentAnswer == s)
        {
            ServerQuestionsList[CurrentQuestion].isCorrect = true;
            return true;
        }
        else
        {
            ServerQuestionsList[CurrentQuestion].isWrong = true;
            return false;
        }

    }

    public IEnumerator SendAnswer(string answer)
    {
        Debug.Log(answer);
        yield return new WaitUntil(() => APIController.instance.isReady);
        yield return new WaitUntil(() => APIController.instance.serverStatus == APIController.ServerStatus.Online);

        string qID = ServerQuestionsList[CurrentQuestion].QuestionId;
        string ans = answer;
        StartCoroutine(APIController.instance.SubmitAnswer(qID, answer, (status) =>
        {

            if (status != null)
            {
                Debug.Log("Answer Debug: " + status.ToString());
            }

        }));


    }

    private void checkForQuestionCompletion()
    {
        if (SavedAnswerdList.Count == CurrentQuestion + 1)
        {
            int i = 0;
            SavedAnswerdList.ForEach((question) =>
            {
                if (question.isCorrect)
                {
                    i += 1;
                }

            });
            if (SavedAnswerdList.Count == i)
            {

                ScreenController.instance.WinScreen.SetActive(true);
               

            }
            else if (SavedAnswerdList.Count != i)
            {

                ScreenController.instance.LoseScreen.SetActive(true);
            }
        }
    }
}
