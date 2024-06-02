using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public Canvas gameOverCanvas; 

    void Start()
    {
        Time.timeScale = 1;
        if (gameOverCanvas == null)
        {
            Debug.LogError("GameOver Canvas is not assigned.");
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key is blocked.");
        }
    }
    public void ShowGameOver()
    {
        Time.timeScale = 0;
        if (gameOverCanvas != null)
        {
            gameOverCanvas.gameObject.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            HideAllCanvasExceptGameOver();
        }
    }

    private void HideAllCanvasExceptGameOver()
    {
        Canvas[] allCanvas = FindObjectsOfType<Canvas>();
        foreach (Canvas canvas in allCanvas)
        {
            if (canvas != gameOverCanvas)
            {
                canvas.gameObject.SetActive(false);
            }
        }
    }
    
}
