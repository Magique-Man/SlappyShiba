using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] ScoreScript _scoreScript;
    [SerializeField] bool isGameOver;
    [SerializeField] bool isPaused;
    [SerializeField] bool isGameStarted;
    [SerializeField] PlayfabManager _playfabManager;
    [SerializeField] GameObject _highScoreObject, _highScoreObjectGO;
    [SerializeField] GameObject _pauseButton;
    [SerializeField] GameObject _gameStartCanvas;
    [SerializeField] GameObject _leaderBoardCanvas;
    [SerializeField] private bool isCleanupRequired;

    [SerializeField] private static float pipeMoveSpeed;

    public bool IsGameOver { get => isGameOver; set => isGameOver = value; }
    public bool IsPaused { get => isPaused; set => isPaused = value; }
    public bool IsGameStarted { get => isGameStarted; set => isGameStarted = value; }
    public bool IsCleanupRequired { get => isCleanupRequired; set => isCleanupRequired = value; }

    private void Awake()
    {
        Time.timeScale = 0.0f;
    }

    private void Start()
    {
        IsCleanupRequired = false;
        _playfabManager = GetComponent<PlayfabManager>();
        pipeMoveSpeed = 1.0f;        
        IsGameStarted = false;
        gameOverCanvas.SetActive(false);
        _gameStartCanvas.SetActive(true);
        _leaderBoardCanvas.SetActive(false);
        //_playfabManager.Login();
    }

    private void Update()
    {
        if(IsGameStarted)
        {
            _gameStartCanvas.SetActive(false);
        }

        if(IsPaused || IsGameOver || !IsGameStarted)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
            
        }

        if(isPaused)
        {
            _highScoreObject.SetActive(true);
        }
        else
        {
            _highScoreObject.SetActive(false);
        }

        if(isGameOver)
        {
            _highScoreObjectGO.SetActive(true);
            _pauseButton.SetActive(false);
            GameOver();
        }
        else 
        {
            _highScoreObjectGO.SetActive(false);
        }
    }

    public void GameOver()
    {
        if(IsCleanupRequired)
        {
            PlayerPrefs.Save();
            _playfabManager.LeaderboardSend(ScoreScript.Score);
            StartCoroutine(LeaderboardUpdate(2.0f));
            IsCleanupRequired = false;
        }
        
        _leaderBoardCanvas.SetActive(true);
        gameOverCanvas.SetActive(true);
    }

    public void RestartLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }



    private void CleanupTasks()
    {

    }

    private void OnApplicationQuit()
    {
    }

    private void OnApplicationPause(bool pause)
    {
        if(pause)
        {
            if(ScoreScript.Score > ScoreScript.HighScore)
            {
                ScoreScript.HighScore = ScoreScript.Score;
                _scoreScript.SetHighScore();
                PlayerPrefs.Save();
            }
        }
    }

    private IEnumerator LeaderboardUpdate(float waitTime)
    {
        //Debug.Log("Coroutine started.");
        yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(waitTime));
        _playfabManager.GetLeaderboard();
        _playfabManager.GetPlayerLeaderboard();
        //Debug.Log("Coroutine ended after " + waitTime + " seconds.");

    }
}

public static class CoroutineUtil
{
    public static IEnumerator WaitForRealSeconds(float waitTime)
    {
        float currentTime = Time.realtimeSinceStartup;
        while(Time.realtimeSinceStartup < currentTime + waitTime)
        {
            yield return null;
        }
    }
}
