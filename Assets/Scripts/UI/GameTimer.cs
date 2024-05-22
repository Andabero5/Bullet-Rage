using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public TMP_Text timeText;
    private float elapsedTime;

    void Start()
    {
        if (timeText == null)
        {
            Debug.LogError("Text component is not assigned.");
            return;
        }
        elapsedTime = 0f;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        
        int minutes = Mathf.FloorToInt(elapsedTime / 60F);
        int seconds = Mathf.FloorToInt(elapsedTime % 60F);
        
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}