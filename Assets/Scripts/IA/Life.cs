using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    public float value = 100;
    public delegate void OnLifeChanged(float newValue);
    public event OnLifeChanged onLifeChanged;

    public void takeDamage(float damage)
    {
        value -= damage;
        if (value < 0)
        {
            value = 0;
        }

        if (onLifeChanged != null)
        {
            onLifeChanged(value);
        }

        UnityEngine.Debug.Log("Vida actual: " + value);
    }
}
