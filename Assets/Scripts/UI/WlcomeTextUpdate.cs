using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WlcomeTextUpdate : MonoBehaviour
{

    public TextMeshProUGUI objText;

    private void Start()
    {
        PlayerPrefs.SetInt("score", 0);
        PlayerPrefs.SetFloat("SavedTime", 0f);
        UpdateTextWithUserName();
    }

    public void UpdateTextWithUserName()
    {
        var userName = PlayerPrefs.GetString("user_name", "");
        string baseText = "Prepárate para adentrarte en la arena, donde serás puesto a prueba contra oleadas de adversarios cada vez más letales. Con un arsenal que evoluciona a tu lado y la posibilidad de cambiar el curso de la batalla con estratégicos power-ups, tu objetivo es simple: acumular puntos y sobrevivir. Pero recuerda, en Bullet Rage, cada enemigo es único, cada enfrentamiento es una oportunidad para demostrar tu valía, y cada momento es una batalla por la supervivencia.";
        objText.text = string.Format("{0}, " + baseText, $"<b>{userName}</b>");
    }
}
