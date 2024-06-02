using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDataManager : MonoBehaviour
{
    public DBManager dbManager;
    public GameTimer gameTimer;
    public void saveInDataBase()
    {
        gameTimer.saveTime();
        DBManager.UserData userUpdated = new DBManager.UserData { username = PlayerPrefs.GetString("user_name", ""), score = PlayerPrefs.GetInt("Score", 0), gameTime = 0 };
        StartCoroutine(dbManager.SaveUserData(userUpdated, (response) =>
        {
            if (response != null)
            {
                Debug.Log("User data saved: " + response);
                reserInfo();
            }
            else
            {
                Debug.LogError("Failed to save user data.");
                reserInfo();
            }
        }));
    }

    void reserInfo()
    {
        PlayerPrefs.SetString("user_name", "");
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetFloat("SavedTime", 0);
    }
  
}
