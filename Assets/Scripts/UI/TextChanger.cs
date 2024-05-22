using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextChanger : MonoBehaviour
{
    public TMP_Text texto;
    public GameObject panel;
    public float changeInterval = 5f;
    private int currentTextIndex = 0;
    private bool isVisible = true;

    private string[] textos = new string[]
    {
        "Presiona 'R' para recargar tu arma.",
        "Usa 'W', 'A', 'S', 'D' para moverte.",
        "Presiona 'Espacio' para saltar.",
        "Presiona 'Esc' para abrir el menú de pausa.",
        "Usa la rueda del ratón para cambiar de arma.",
    };

    void Start()
    {
        if (texto == null)
        {
            Debug.LogError("Text component is not assigned.");
            return;
        }

        StartCoroutine(ChangeTextRoutine());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isVisible = !isVisible;
            panel.gameObject.SetActive(isVisible);
        }
    }

    IEnumerator ChangeTextRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(changeInterval);
            
            currentTextIndex = (currentTextIndex + 1) % textos.Length;
            texto.text = textos[currentTextIndex];
        }
    }
}