using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuizManager : MonoBehaviour
{
    public Text questionText;
    public Button[] answerButtons;
    public Text endGame;

    private int currentQuestionIndex;
    private Question[] questions;

    private void Start()
    {
        questions = new Question[]
        {
            new Question("What is the capital of France?", new string[] { "Paris", "London", "Berlin", "Rome" }, 0),
            new Question("What is 2 + 2?", new string[] { "3", "4", "5", "6" }, 1),
            new Question("Qual é a capital do Brasil?", new string[] {"São Paulo", "Rio de Janeiro", "Brasília", "Acre"}, 2),
        };

        currentQuestionIndex = 0;
        endGame.gameObject.SetActive(false);
        DisplayQuestion();

    }

    private void DisplayQuestion()
    {
        Question currentQuestion = questions[currentQuestionIndex];
        questionText.text = currentQuestion.questionText;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<Text>().text = currentQuestion.answers[i];
            answerButtons[i].GetComponent<Image>().color = Color.white;
            int answerIndex = i;
            answerButtons[i].onClick.RemoveAllListeners(); // Clear previous listeners
            answerButtons[i].onClick.AddListener(() => CheckAnswer(answerIndex));
        }
    }

    private void CheckAnswer(int answerIndex)
    {

        if (answerIndex == questions[currentQuestionIndex].correctAnswerIndex)
        {
            answerButtons[answerIndex].GetComponentInChildren<Text>().text = "Correct!";
            answerButtons[answerIndex].GetComponent<Image>().color = Color.green;
        }
        else
        {
            answerButtons[answerIndex].GetComponentInChildren<Text>().text = "Incorrect!";
            answerButtons[answerIndex].GetComponent<Image>().color = Color.red;
        }

        
        // Move to the next question after a delay
        Invoke("NextQuestion", 2);
    }


    private void NextQuestion()
    {
        currentQuestionIndex++;
        if (currentQuestionIndex < questions.Length)
        {
            DisplayQuestion();
        }
        else
        {
            endGame.gameObject.SetActive(true);

            endGame.text = "Fim";
        }
    }
}


[System.Serializable]
public class Question
{
    public string questionText;
    public string[] answers;
    public int correctAnswerIndex;

    public Question(string questionText, string[] answers, int correctAnswerIndex)
    {
        this.questionText = questionText;
        this.answers = answers;
        this.correctAnswerIndex = correctAnswerIndex;
    }
}

