using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIHandler : MonoBehaviour
{
    public Text highScoreText, scoreText;
    public GameObject gameOverObject;

    // Start is called before the first frame update
    void Start()
    {
        // Display the best score if it exists
        if(ScoreManager.bestScore==null)
        {
            highScoreText.text = "No high score found!";
        }
        else
        {
            highScoreText.text = $"Best Score: {ScoreManager.bestScore.score} – {ScoreManager.bestScore.name}";
        }
        
    }

    public void DisplayGameOver()
    {
        gameOverObject.SetActive(true);
    }

    public void UpdateScore(int points)
    {
        scoreText.text = $"Score : {points}";
    }
}
