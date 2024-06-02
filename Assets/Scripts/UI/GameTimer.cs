using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public TMP_Text timeText;
    private float elapsedTime;
    private bool isPaused;

    void Start()
    {
        if (timeText == null)
        {
            Debug.LogError("Text component is not assigned.");
            return;
        }
        elapsedTime = PlayerPrefs.GetFloat("SavedTime", 0f); // Cargar el tiempo guardado si existe
        isPaused = false;
        UpdateTimeText();
    }

    void Update()
    {
        if (!isPaused)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimeText();
        }
    }

    public void saveTime()
    {
        isPaused = true;
        PlayerPrefs.SetFloat("SavedTime", elapsedTime);
        PlayerPrefs.Save();
    }

    public void ResumeTime()
    {
        isPaused = false;
    }

    void UpdateTimeText()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60F);
        int seconds = Mathf.FloorToInt(elapsedTime % 60F);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}