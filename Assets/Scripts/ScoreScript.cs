using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    private static int score;
    private int currentScore;
    private static int highScore;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private TMP_Text highScoreTextGO;

    public static int Score { get => score; set => score = value; }
    public static int HighScore { get => highScore; set => highScore = value; }

    private void Start()
    {
        //scoreText = GetComponent<TMP_Text>();
        Score = 0;
        GetHighScore();
    }

    private void Update()
    {
        scoreText.text = Score.ToString();
        highScoreText.text = HighScore.ToString();
        highScoreTextGO.text = HighScore.ToString();

        if(score > HighScore)
        {
            HighScore = score;
            SetHighScore();
        }
    }

    public void GetHighScore()
    {
        Debug.Log("Get High Score");
        HighScore = PlayerPrefs.GetInt("high_score", 0);
    }

    public void SetHighScore()
    {
        Debug.Log("Set High Score");
        PlayerPrefs.SetInt("high_score", HighScore);        
    }

}
