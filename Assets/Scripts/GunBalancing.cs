using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class GunBalancing : MonoBehaviour
{
    public float quantity;
    public float maxQuantity;
    public float time;
    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float movX = Input.GetAxis("Mouse X") * quantity;
        float movY = Input.GetAxis("Mouse Y") * quantity;

        movX = Mathf.Clamp(movX, -maxQuantity, maxQuantity);
        movY = Mathf.Clamp(movY, -maxQuantity, maxQuantity);

        Vector3 finalPositionMovement = new Vector3(movX, movY, 0);

        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPositionMovement + initialPosition, time * Time.deltaTime);
    }
}
