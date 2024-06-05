using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CinematicToFPS : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeSceneAfterDelay(27f));
    }

    IEnumerator ChangeSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(3);
    }
}
