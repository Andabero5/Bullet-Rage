using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;


[CreateAssetMenu(fileName = "DatabaseManager", menuName = "ScriptableObjects/DatabaseManager", order = 1)]
public class DBManager : ScriptableObject
{
    public string baseUrl = "http://localhost/db.php";

    public IEnumerator CreateUser(UserData userData, Action<string> onResponse)
    {
        WWWForm form = userData.ToForm();
        using (UnityWebRequest www = UnityWebRequest.Post(baseUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + www.error);
                onResponse?.Invoke(www.error);
            }
            else
            {
                Debug.Log("Response: " + www.downloadHandler.text);
                onResponse?.Invoke(www.downloadHandler.text);
            }
        }
    }

    public IEnumerator GetTopScorer(Action<string> onResponse)
    {
        UnityWebRequest request = UnityWebRequest.Get(baseUrl + "?action=getTopScorer");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("GetTopScorer Error: " + request.error);
            onResponse?.Invoke(null);
        }
        else
        {
            Debug.Log("GetTopScorer Response: " + request.downloadHandler.text);
            onResponse?.Invoke(request.downloadHandler.text);
        }
    }

    public IEnumerator GetUserBestScore(string username, Action<string> onResponse)
    {
        UnityWebRequest request = UnityWebRequest.Get(baseUrl + "?action=getUserBestScore&username=" + username);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("GetUserBestScore Error: " + request.error);
            onResponse?.Invoke(null);
        }
        else
        {
            Debug.Log("GetUserBestScore Response: " + request.downloadHandler.text);
            onResponse?.Invoke(request.downloadHandler.text);
        }
    }

    public IEnumerator GetScores(Action<string> onResponse)
    {
        UnityWebRequest request = UnityWebRequest.Get(baseUrl + "?action=getScores");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("GetScores Error: " + request.error);
            onResponse?.Invoke(null);
        }
        else
        {
            Debug.Log("GetScores Response:  " + request.downloadHandler.text);
            onResponse?.Invoke(request.downloadHandler.text);
        }
    }
    
    public IEnumerator UpdateUser(UserData userData, Action<string> onResponse)
    {
        WWWForm form = userData.ToForm();
        form.AddField("action", "updateUser");
        using (UnityWebRequest www = UnityWebRequest.Post(baseUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + www.error);
                onResponse?.Invoke(www.error);
            }
            else
            {
                Debug.Log("Response: " + www.downloadHandler.text);
                onResponse?.Invoke(www.downloadHandler.text);
            }
        }
    }
    [System.Serializable]
    public class UserData
    {
        public string username;
        public int score;
        public int gameTime;

        public WWWForm ToForm()
        {
            WWWForm form = new WWWForm();
            form.AddField("username", username);
            form.AddField("score", score);
            form.AddField("game_time", gameTime);
            return form;
        }
    }
}