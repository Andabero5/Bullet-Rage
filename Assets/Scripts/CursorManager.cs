
using System.Collections.Generic;
using UnityEngine;


using UnityEngine.SceneManagement;

public class CursorManager : MonoBehaviour
{
    public List<string> scenesToShowCursor; 

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CheckCursorState(scene.name);
    }

    void CheckCursorState(string sceneName)
    {
        if (scenesToShowCursor.Contains(sceneName))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
