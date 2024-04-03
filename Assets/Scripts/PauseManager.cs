using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
   public bool pause;
   public GameObject pauseMenu;

   private void Start()
   {
      pauseMenu.SetActive(false);
      Time.timeScale = 1;
   }

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.Escape))
      {
         Pause();
      }
   }

   public void Pause()
   {
      pause = !pause;
      pauseMenu.SetActive(pause);
      if (pause)
      {
         Cursor.visible = true;
         Cursor.lockState = CursorLockMode.None;
         Time.timeScale = 0;
      }
      else
      {
         Cursor.visible = false;
         Cursor.lockState = CursorLockMode.Locked;
         Time.timeScale = 1;
      }
   }

   public void Menu()
   {
      SceneManager.LoadScene(0);
   }
}
