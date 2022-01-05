using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class PlayfabManager : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] GameObject _nameInputCanvas;
    [SerializeField] GameObject _leaderboardCanvas;
    [SerializeField] GameObject _mainMenuCanvas;
    [SerializeField] GameObject _loadingCanvas;


    [SerializeField] private GameObject _row;
    [SerializeField] private Transform _rowParent;
    [SerializeField] private Transform _playerRowParent;

    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private GameObject _errorText;

    public void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            } 
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnError);
    }

    public void RandomLogin()
    {
        var request = new LoginWithCustomIDRequest 
        { 
            CustomId = Random.Range(100000, 1000000000).ToString(), 
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnError);
    }

    void OnLoginSuccess(LoginResult result)
    {

        Debug.Log("Successful login or account creation.");

        string name = null;

        if(result.InfoResultPayload.PlayerProfile != null)
        {
            name = result.InfoResultPayload.PlayerProfile.DisplayName;
        }

        if(name == null)
        {
            _loadingCanvas.SetActive(false);
            SetupNewUsername();
        }
        else
        {
            _loadingCanvas.SetActive(false);
            //_leaderboardCanvas.SetActive(true);
            StartMainMenu();
        }
        //StartCoroutine(CloseLoading());
    }

    private void SetupNewUsername()
    {
        _errorText.SetActive(false);
        _nameInputCanvas.SetActive(true);
    }

    private void StartMainMenu()
    {
        _leaderboardCanvas.SetActive(false);
        _nameInputCanvas.SetActive(false);
        _loadingCanvas.SetActive(false);
        _mainMenuCanvas.SetActive(true);
    }

    IEnumerator CloseLoading()
    {
        yield return new WaitForSeconds(1);
        _loadingCanvas.SetActive(false);
        _mainMenuCanvas.SetActive(true);
    }

    public void LeaderboardSend(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> 
            { 
                new StatisticUpdate 
                { 
                    StatisticName = "Score", 
                    Value = score 
                } 
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successful, leaderboard result sent.");
    }

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "Score",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }
    
    void OnLeaderboardGet(GetLeaderboardResult result)
    {
        foreach(Transform item in _rowParent)
        {
            Destroy(item.gameObject);
        }

        foreach(var item in result.Leaderboard)
        {
            GameObject newRow = Instantiate(_row, _rowParent);
            TMP_Text[] rowText = newRow.GetComponentsInChildren<TMP_Text>();
            rowText[0].text = (1 + item.Position).ToString();
            //rowText[1].text = item.PlayFabId;
            rowText[1].text = item.DisplayName;
            rowText[2].text = item.StatValue.ToString();

            //Debug.Log(item.Position + " " + item.PlayFabId + " " + item.StatValue);
        }
    }

    public void GetPlayerLeaderboard()
    {
        var request = new GetLeaderboardAroundPlayerRequest
        {
            StatisticName = "Score",
            MaxResultsCount = 1
        };
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnPlayerLeaderboardGet, OnError);
    }

    void OnPlayerLeaderboardGet(GetLeaderboardAroundPlayerResult result)
    {
        foreach(Transform item in _playerRowParent)
        {
            Destroy(item.gameObject);
        }

        foreach(var item in result.Leaderboard)
        {
            GameObject newRow = Instantiate(_row, _playerRowParent);
            TMP_Text[] rowText = newRow.GetComponentsInChildren<TMP_Text>();
            rowText[0].text = (1 + item.Position).ToString();
            //rowText[1].text = item.PlayFabId;
            rowText[1].text = item.DisplayName;
            rowText[2].text = item.StatValue.ToString();

            Debug.Log(item.Position + " " + item.PlayFabId + " " + item.StatValue);
        }

    }

    public void RandomLeaderboardSend()
    {
        int randScore = Random.Range(1, 150);

        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> 
            { 
                new StatisticUpdate 
                { 
                    StatisticName = "Score", Value = randScore 
                } 
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    public void SubmitUsername()
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = _inputField.text,
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnUsernameError);
    }
    
    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Updated username!");
        StartMainMenu();

    }

    void OnUsernameError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
        _errorText.SetActive(true);
    }

    void OnError(PlayFabError error)
    {
        //Debug.Log("Login or account creation error.");
        Debug.Log(error.GenerateErrorReport());
    }
}
