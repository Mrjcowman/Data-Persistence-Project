using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreUiHandler : MonoBehaviour
{

    public InputField nameInput;
    public Text namesField, scoresField, currentScoreField;

    // Start is called before the first frame update
    void Start()
    {
        DisplayScores();
    }

    public void OnPlayAgainButtonPressed()
    {
        ScoreManager.currentPoints = 0;
        SceneManager.LoadScene("main");
    }

    public void OnMenuButtonPressed()
    {
        ScoreManager.currentPoints = 0;
        SceneManager.LoadScene("menu");
    }

    public void OnSubmitButtonPressed()
    {
        string name = nameInput.text;
        ScoreManager.instance.PostCurrentScore(name);
        ScoreManager.instance.SaveHighScores();
        nameInput.text = "";
        DisplayScores();
    }

    public void DisplayScores()
    {
        string names = "";
        string scores = "";
        foreach(ScoreManager.ScoreLog score in ScoreManager.highScores)
        {
            names += score.name + "\n";
            scores += score.score.ToString() + "\n";
        }
        namesField.text = names;
        scoresField.text = scores;
        currentScoreField.text = "Your Score: " + ScoreManager.currentPoints;
    }
}
