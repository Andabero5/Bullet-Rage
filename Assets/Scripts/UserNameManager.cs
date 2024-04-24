using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayText : MonoBehaviour
{
    public TextMeshProUGUI objText = null;  
    public TMP_InputField  display = null;

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
        Debug.Log("entro al create");
        PlayerPrefs.Save();
    }

    public void UpdateTextWithUserName(String userName)
    {
        objText.text = string.Format(objText.text, $"<b>{userName}</b>");
    }
}
