using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChanceScenesWithButton : MonoBehaviour
{
    public int levelIndex;

    public static void LoadScene(int index)
    {
        try
        {
            SceneManager.LoadScene(index);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
  
    public static void QuitGame()
    {
        Application.Quit();
    }
    
}
