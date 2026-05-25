using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New QuestionData", menuName = "QuestionData")]
public class QtsData : ScriptableObject
{
    [System.Serializable]
    public struct Question
    {
        public string questionText;
        public string[] replies;
        public int correctReplyIndex;
    }

    public Question[] questions;

    private List<int> usedQuestions = new List<int>();

    public Question GetRandomQuestion()
    {
        if (usedQuestions.Count >= questions.Length)
        {
            usedQuestions.Clear();
        }

        int randomIndex;

        do
        {
            randomIndex = Random.Range(0, questions.Length);
        }
        while (usedQuestions.Contains(randomIndex));

        usedQuestions.Add(randomIndex);

        return questions[randomIndex];
    }
}