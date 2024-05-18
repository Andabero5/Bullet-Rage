using System;
using TMPro;
using UnityEngine;

public class DisplayText : MonoBehaviour
{
    public TextMeshProUGUI objText = null;  
    public TMP_InputField  display = null;
    public DBManager dbManager;

    private void Start()
    {
        if (objText!= null)
        {
            string userName = PlayerPrefs.GetString("user_name", "Jugador").ToUpper();
            UpdateTextWithUserName(userName);
        }
    }

    public void Create()
    {
        string userName = display.text;
        PlayerPrefs.SetString("user_name", userName);
        if (dbManager != null)
        {
            DBManager.UserData newUser = new DBManager.UserData { username = userName, score = 0, gameTime = 0 };
            StartCoroutine(dbManager.CreateOrUpdateUser(newUser, (response) => {
                if (response.Contains("Username already exists"))
                {
                    Debug.LogError("This username is already taken. Please choose another.");
                }
                else
                {
                    PlayerPrefs.SetString("user_name", userName);
                    PlayerPrefs.Save();
                    UpdateTextWithUserName(userName);
                    Debug.Log("User created successfully!");
                }
            }));
        }
        PlayerPrefs.Save();
    }

    public void UpdateTextWithUserName(String userName)
    {
        objText.text = string.Format(objText.text, $"<b>{userName}</b>");
    }
}
