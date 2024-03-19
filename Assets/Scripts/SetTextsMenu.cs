using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class SetTextsMenu : MonoBehaviour
{
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI bestTimeText;
    public TextMeshProUGUI lastScoreText;
    public TextMeshProUGUI lastTimeText;
    private void Start()
    {
        SetTexts();
    }

    private void SetTexts()
    {
        int bestScore = GetBestScore();
        float bestTime = GetBestTime();
        
        bestScoreText.text = $"Best Score: {bestScore}";
        bestTimeText.text = $"Best Time: {FormatTime(bestTime)}";
        
        List<int> lastScores = GetListFromPlayerPrefs<int>("LastScores");
        List<float> lastTimes = GetListFromPlayerPrefs<float>("LastTimes");
        if (lastScores.Count > 0 && lastTimes.Count > 0)
        {
            lastScoreText.text = $"Last Score: {lastScores.Last()}";
            lastTimeText.text = $"Last Time: {FormatTime(lastTimes.Last())}";
        }
    }

    private string FormatTime(float timeInSeconds)
    {
        int minutes = (int)timeInSeconds / 60;
        int seconds = (int)timeInSeconds % 60;
        return $"{minutes:00}:{seconds:00}";
    }

    private int GetBestScore()
    {
        List<int> lastScores = GetListFromPlayerPrefs<int>("LastScores");
        return lastScores != null && lastScores.Count > 0 ? lastScores.Max() : 0;
    }

    private float GetBestTime()
    {
        List<float> lastTimes = GetListFromPlayerPrefs<float>("LastTimes");
        return lastTimes != null && lastTimes.Count > 0 ? lastTimes.Max() : float.MaxValue;
    }

    private List<T> GetListFromPlayerPrefs<T>(string key)
    {
        string json = PlayerPrefs.GetString(key, "[]");
        return JsonUtility.FromJson<ListWrapper<T>>(json).list;
    }

    [System.Serializable]
    private class ListWrapper<T>
    {
        public List<T> list;
    }


}
