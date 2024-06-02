using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserDataManager : MonoBehaviour
{
    public DBManager dbManager;
    public GameTimer gameTimer;

    public void saveInDataBase(string scene)
    {
        gameTimer.saveTime();

        string userName = PlayerPrefs.GetString("user_name", "");
        int score = PlayerPrefs.GetInt("score", 0);
        int gameTime =
            Mathf.FloorToInt(gameTimer.GetElapsedTime());

        if (string.IsNullOrEmpty(userName))
        {
            Debug.LogError("User name is empty");
            return;
        }

        DBManager.UserData userUpdated = new DBManager.UserData
            { username = userName, score = score, gameTime = gameTime };

        StartCoroutine(dbManager.UpdateUser(userUpdated, (response) =>
        {
            if (!string.IsNullOrEmpty(response))
            {
                Debug.Log("User data saved: " + response);
            }
            else
            {
                Debug.LogError("Failed to save user data.");
            }

            reserInfo();
            changeScene(scene);
        }));
    }

    private void changeScene(string scene)
    {
        if (scene == "quit")
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(scene);
        }
    }

    private void reserInfo()
    {
        PlayerPrefs.SetInt("score", 0);
        PlayerPrefs.SetFloat("SavedTime", 0);
    }
}
