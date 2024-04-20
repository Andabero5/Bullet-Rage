using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLogic : MonoBehaviour
{
    public Life life;
    public bool isLife0 = false;
    [SerializeField] private Animator animatorLose;

    // Start is called before the first frame update
    void Start()
    {
        life = GetComponent<Life>();
    }

    // Update is called once per frame
    void Update()
    {
        checkLife();
    }

    void checkLife()
    {
        if (isLife0) return;
        if (life.value <= 0)
        {
            isLife0 = true;
        }
    }

    void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
