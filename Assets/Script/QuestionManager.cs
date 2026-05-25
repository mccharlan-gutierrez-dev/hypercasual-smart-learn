using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    public Text questionText;
    public Text scoreText;
    public Text finalScore;
    public Text timeText;

    public Button[] replyButtons;

    public QtsData qtsData;

    public GameObject right;
    public GameObject wrong;
    public GameObject gameFinished;
    public GameObject questionPanel;

    private int score = 0;

    private float timeLeft = 10f;
    private bool timerRunning = false;

    // RANDOM QUESTION VARIABLES
    private List<int> usedQuestions = new List<int>();
    private int currentQuestion;

    void Start()
    {
        scoreText.text = "0";

        right.SetActive(false);
        wrong.SetActive(false);
        gameFinished.SetActive(false);

        LoadRandomQuestion();
        StartTimer();
    }

    void Update()
    {
        if (timerRunning)
        {
            timeLeft -= Time.deltaTime;

            int minutes = Mathf.FloorToInt(timeLeft / 60);
            int seconds = Mathf.FloorToInt(timeLeft % 60);

            timeText.text = minutes + ":" + seconds.ToString("00");

            if (timeLeft <= 0)
            {
                timerRunning = false;
                GameOverTime();
            }
        }
    }

    void StartTimer()
    {
        timeLeft = 60f;
        timerRunning = true;
    }

    void LoadRandomQuestion()
    {
        if (usedQuestions.Count >= qtsData.questions.Length)
        {
            FinishGame();
            return;
        }

        do
        {
            currentQuestion = Random.Range(0, qtsData.questions.Length);
        }
        while (usedQuestions.Contains(currentQuestion));

        usedQuestions.Add(currentQuestion);

        SetQuestion(currentQuestion);
    }

    void SetQuestion(int questionIndex)
    {
        questionText.text = qtsData.questions[questionIndex].questionText;

        foreach (Button r in replyButtons)
        {
            r.onClick.RemoveAllListeners();
        }

        for (int i = 0; i < replyButtons.Length; i++)
        {
            replyButtons[i].GetComponentInChildren<Text>().text =
                qtsData.questions[questionIndex].replies[i];

            int replyIndex = i;

            replyButtons[i].onClick.AddListener(() =>
            {
                CheckReply(replyIndex);
            });
        }
    }

    void CheckReply(int replyIndex)
    {
        timerRunning = false;

        if (replyIndex == qtsData.questions[currentQuestion].correctReplyIndex)
        {
            score++;
            scoreText.text = score.ToString();

            right.SetActive(true);

            SoundManager.instance.PlayCorrect();
        }
        else
        {
            wrong.SetActive(true);

            SoundManager.instance.PlayWrong();
        }

        foreach (Button r in replyButtons)
        {
            r.interactable = false;
        }

        StartCoroutine(Next());
    }

    IEnumerator Next()
    {
        yield return new WaitForSeconds(2);

        ResetQuestion();
    }

    void ResetQuestion()
    {
        right.SetActive(false);
        wrong.SetActive(false);

        foreach (Button r in replyButtons)
        {
            r.interactable = true;
        }

        LoadRandomQuestion();
        StartTimer();
    }

    void FinishGame()
    {
        timerRunning = false;

        right.SetActive(false);
        wrong.SetActive(false);

        questionPanel.SetActive(false);

        foreach (Button r in replyButtons)
        {
            r.gameObject.SetActive(false);
        }

        gameFinished.SetActive(true);

        float scorePercentage = (float)score / qtsData.questions.Length * 100;

        finalScore.text = "You scored " + scorePercentage.ToString("F0") + "%";

        if (scorePercentage < 50)
        {
            finalScore.text += "\nFailed";
        }
        else if (scorePercentage < 60)
        {
            finalScore.text += "\nGood";
        }
        else if (scorePercentage < 70)
        {
            finalScore.text += "\nVery Good";
        }
        else if (scorePercentage < 80)
        {
            finalScore.text += "\nSatisfactory";
        }
        else
        {
            finalScore.text += "\nExcellent!";
        }

        SoundManager.instance.PlayFinish();
    }

    void GameOverTime()
    {
        timerRunning = false;

        right.SetActive(false);
        wrong.SetActive(false);

        questionPanel.SetActive(false);

        foreach (Button r in replyButtons)
        {
            r.gameObject.SetActive(false);
        }

        gameFinished.SetActive(true);

        finalScore.text = "Time's Up!\nYour Score: " + score;
    }
}