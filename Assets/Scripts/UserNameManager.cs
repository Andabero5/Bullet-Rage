using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class DisplayText : MonoBehaviour
{
    public TextMeshProUGUI objText;
    public TMP_InputField display;
    public DBManager dbManager;
    public Button createButton; 
    
    public GameObject alertPanel;  
    public TextMeshProUGUI alertText;
    public Button alertButton;

    private bool userExists = true;
    private void Start()
    {
        alertPanel.SetActive(false);
        alertButton.onClick.AddListener(OnAlertButtonClick);
        createButton.onClick.AddListener(OnCreateButtonClick);
        if (objText != null)
        {
            string userName = PlayerPrefs.GetString("user_name", "").ToUpper();
            UpdateTextWithUserName(userName);
        }
    }

    private void Create()
    {
        string userName = display.text;
        PlayerPrefs.SetString("user_name", userName);
        if (userName == "")
        {
            ShowAlert("Rellena el campo");
        }
        if (dbManager != null)
        {
            DBManager.UserData newUser = new DBManager.UserData { username = userName, score = 0, gameTime = 0 };
            StartCoroutine(dbManager.CreateOrUpdateUser(newUser, (response) => {
                if (response.Contains("Username already exists"))
                {
                    Debug.LogError("This username is already taken. Please choose another.");
                    ShowAlert("El usuario ya existe. Por favor, elige otro nombre.");
                }
                else
                {
                    SceneManager.LoadScene("Menú");
                    PlayerPrefs.SetString("user_name", userName);
                    PlayerPrefs.Save();
                    UpdateTextWithUserName(userName);
                    Debug.Log("User created successfully!");
                }
            }));
        }
        PlayerPrefs.Save();
    }

    private void ShowAlert(string message)
    {
        alertText.text = message;
        alertPanel.SetActive(true);
    }

    private void OnAlertButtonClick()
    {
        alertPanel.SetActive(false);
        display.text = "";
    }
    private void OnCreateButtonClick()
    {
        if (display.text == "")
        {
            ShowAlert("Rellena el campo");
        }
        else if (!userExists)
        {
            SceneManager.LoadScene("Menú");
        }else if(userExists)
        {
            Create();
        }
        
       
    }
    

    public void UpdateTextWithUserName(string userName)
    {
        objText.text = string.Format(objText.text, $"<b>{userName}</b>");
    }
}
