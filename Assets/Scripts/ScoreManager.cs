using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public static ScoreLog[] highScores;
    public static int currentPoints { get; set; }
    public static ScoreLog bestScore { get { return highScores[0]; } }

    private void Awake()
    {
        // Singleton enforcement
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
        LoadHighScores();
    }

    [System.Serializable]
    public class ScoreLog
    {
        public string name;
        public int score;

        public ScoreLog(string name = "", int score = 0)
        {
            this.name = name;
            this.score = score;
        }
    }

    [System.Serializable]
    public class SaveData
    {
        public ScoreLog[] scores;
    }

    public void PostCurrentScore(string name)
    {
        ScoreLog newScore = new ScoreLog(name, currentPoints);

        // Add the first score to the array
        if (highScores[0] == null)
        {
            highScores[0] = newScore;
            return;
        }

        // Find the position this score would fit
        int scoreIndex = System.Array.FindIndex<ScoreLog>(highScores, x => x.score < currentPoints);

        // If there is a score on the leaderboard lower than the new one
        if (scoreIndex > -1)
        {
            // Shift every score at a higher index (lower rank) by one space and insert the new score
            for (int i = highScores.Length - 1; i > scoreIndex && i > 0; i--)
            {
                highScores[i] = highScores[i - 1];
            }
            highScores[scoreIndex] = newScore;
        }
        
    }

    public void SaveHighScores()
    {
        if (highScores == null) return;

        SaveData data = new SaveData();
        data.scores = highScores;

        string path = Application.persistentDataPath + "/highscores.json";
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
    }

    public void LoadHighScores()
    {
        // Initialize highScores if it doesn't exist
        if (highScores == null)
        {
            highScores = new ScoreLog[10];
            for(int i= 0; i < highScores.Length; i++)
            {
                highScores[i] = new ScoreLog();
            }
        }

        // Load scores from json file
        string path = Application.persistentDataPath + "/highscores.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            // Copy data and trim overflow
            for (int i = 0; i < Mathf.Min(highScores.Length, data.scores.Length); i++)
            {
                highScores[i] = data.scores[i];
            }
        }
    }

}
