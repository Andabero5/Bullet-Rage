using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalLogic : MonoBehaviour
{
    public GameObject portal;
    
    private int currentScore;

    // Start is called before the first frame update
    void Start()
    {
        portal.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        currentScore = PlayerPrefs.GetInt("score", 0);
        
        if (currentScore >= 1200)
        {
            portal.SetActive(true);
        }
    }
}
