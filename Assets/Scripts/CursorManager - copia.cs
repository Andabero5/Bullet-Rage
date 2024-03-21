
using UnityEngine;


using UnityEngine.SceneManagement;

public class CursorManager : MonoBehaviour
{
    public string sceneToShowCursor;

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
        if (sceneName == sceneToShowCursor)
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
