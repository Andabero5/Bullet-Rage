using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChanceScenesWithButton : MonoBehaviour
{
    public int levelIndex;
    public bool nextLevel;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadScene(levelIndex);
            if (levelIndex == 0)
            {
                PlayerPrefs.SetInt("lives", 3);
                PlayerPrefs.SetInt("score", 0);
                PlayerPrefs.Save();
            }
        }
        
    }



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
  
    
    
}
