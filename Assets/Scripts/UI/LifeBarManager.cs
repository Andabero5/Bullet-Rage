using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine;
using UnityEngine.UI;

public class LifeBarManager : MonoBehaviour
{
    public Image lifeBar;
    public Life playerLife;
    public float maxLife = 100; // Máxima vida para el cálculo del fillAmount

    void Start()
    {
        if (playerLife != null)
        {
            playerLife.onLifeChanged += UpdateLifeBar;
            UpdateLifeBar(playerLife.value); // Inicializar la barra de vida
        }
        else
        {
            Debug.LogError("Player Life reference is not assigned in LifeManager.");
        }
    }

    void UpdateLifeBar(float newLifeValue)
    {
        lifeBar.fillAmount = newLifeValue / maxLife;
        Debug.Log("Actualizando barra de vida: " + lifeBar.fillAmount);
    }
}