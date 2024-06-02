using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class SetTextsMenu : MonoBehaviour
{
    public TextMeshProUGUI topScorerNameText;
    public TextMeshProUGUI topScorerScoreText;
    public TextMeshProUGUI topScorerTimeText;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI bestTimeText;
    public TextMeshProUGUI scoresText;
    public DBManager dbManager;

    private string currentUsername = "";

    private void Start()
    {
        if (dbManager != null)
        {
            currentUsername = PlayerPrefs.GetString("user_name", "");
            Debug.Log("el nombre de usuario es" + currentUsername);
            StartCoroutine(dbManager.GetTopScorer(ProcessTopScorer));
            StartCoroutine(dbManager.GetUserBestScore(currentUsername, ProcessUserBestScore));
            StartCoroutine(dbManager.GetScores(ProcessScores));
        }
    }

    private void ProcessTopScorer(string json)
    {
        if (!string.IsNullOrEmpty(json))
        {
            var topScorerData = JsonUtility.FromJson<TopScorerData>(json);
            if (topScorerData != null)
            {
                topScorerNameText.text = topScorerData.username;
                topScorerScoreText.text = topScorerData.score.ToString();
                topScorerTimeText.text = FormatTime(topScorerData.game_time);
            }
            else
            {
                Debug.LogError("Failed to deserialize top scorer data.");
            }
        }
        else
        {
            Debug.LogError("Received empty or null JSON string");
        }
    }

    private void ProcessUserBestScore(string json)
    {
        if (!string.IsNullOrEmpty(json))
        {
            var userBestScoreData = JsonUtility.FromJson<UserBestScoreData>(json);
            if (userBestScoreData != null)
            {
                bestScoreText.text = userBestScoreData.score.ToString();
                bestTimeText.text = FormatTime(userBestScoreData.game_time);
            }
            else
            {
                Debug.LogError("Failed to deserialize user best score data.");
            }
        }
        else
        {
            Debug.LogError("Received empty or null JSON string");
        }
    }

    private void ProcessScores(string json)
    {
        if (!string.IsNullOrEmpty(json))
        {
            PlayerDataContainer dataContainer = JsonUtility.FromJson<PlayerDataContainer>(json);
            if (dataContainer != null && dataContainer.players != null)
            {
                string displayText = "<mspace=0.6em>Username          Score    Time\n</mspace>";
                foreach (PlayerData player in dataContainer.players)
                {
                    displayText += $"<mspace=0.6em>{player.username,-16} {player.score,5}   {FormatTime(player.game_time),-5}\n</mspace>";
                }

                if (scoresText != null)
                {
                    scoresText.text = displayText; 
                }
            }
            else
            {
                Debug.LogError("Failed to deserialize scores data.");
            }
        }
        else
        {
            Debug.LogError("Received empty or null JSON string");
        }
    }

    private string FormatTime(float timeInSeconds)
    {
        if (timeInSeconds == 0)
            return "00:00";
        int minutes = (int)timeInSeconds / 60;
        int seconds = (int)timeInSeconds % 60;
        return $"{minutes:00}:{seconds:00}";
    }

    [System.Serializable]
    public class TopScorerData
    {
        public string username;
        public int score;
        public float game_time;
    }

    [System.Serializable]
    public class UserBestScoreData
    {
        public int score;
        public float game_time;
    }

    [System.Serializable]
    public class PlayerData
    {
        public string username;
        public int score;
        public float game_time;
    }

    [System.Serializable]
    public class PlayerDataContainer
    {
        public List<PlayerData> players;
    }
}
