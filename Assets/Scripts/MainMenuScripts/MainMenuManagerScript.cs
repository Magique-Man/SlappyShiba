using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuManagerScript : MonoBehaviour
{
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private int highScore;
    [SerializeField] private float _timeScale;
    [SerializeField] private PlayfabManager _playfabManager;
    [SerializeField] private int opened_amount;

    private void Awake()
    {
    }

    private void Start()
    {
        opened_amount = PlayerPrefs.GetInt("opened_amount", 0);

        if(opened_amount < 1)
        {
            Debug.Log("Increment Opened Amount");
            opened_amount++;
            PlayerPrefs.SetInt("opened_amount", opened_amount);
            PlayerPrefs.SetInt("high_score", 0);
        }

        _playfabManager = GetComponent<PlayfabManager>();
        _playfabManager.Login();
        GetHighScore();
        highScoreText.text = highScore.ToString();
        Time.timeScale = _timeScale;
    }

    private void Update()
    {
        Time.timeScale = _timeScale;
    }

    public void GetHighScore()
    {
        //Debug.Log("<Main Menu> Get High Score.");
        highScore = PlayerPrefs.GetInt("high_score", 0);
    }
}
