using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player; // Referencia al jugador
    public float defaultMouseSensitivity = 500f;
    private bool isThirdPerson = true; // Controla si la vista es en tercera o primera persona
    private float yaw = 0f;
    private float pitch = 0f;
    public float pitchMin = -30f;
    public float pitchMax = 60f;

   
    public float thirdPersonDistance = 5f;
    public float thirdPersonHeight = 5f; 
    
    private Vector3 firstPersonOffset = new Vector3(0, 6f, 0);

    void Start()
    {
        yaw = player.transform.eulerAngles.y + 180f;
        pitch = 0f;
    }

    void LateUpdate()
    {
        CameraControl();
    }

    void CameraControl()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isThirdPerson = !isThirdPerson;
        }

        // Aquí solo ajustas 'pitch' basado en el movimiento vertical del mouse
        float mouseY = Input.GetAxis("Mouse Y") * defaultMouseSensitivity * Time.deltaTime;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);

        if (isThirdPerson)
        {
            // En tercera persona
            Vector3 positionOffset = Quaternion.Euler(pitch, player.transform.eulerAngles.y, 0f) * new Vector3(0, 0, -thirdPersonDistance);
            Vector3 targetPosition = player.transform.position + new Vector3(0, thirdPersonHeight, 0) + positionOffset;
            transform.position = targetPosition;
            transform.LookAt(player.transform.position + new Vector3(0, thirdPersonHeight, 0));
        }
        else
        {
            // En primera persona
            transform.position = player.transform.position + firstPersonOffset;
            transform.rotation = Quaternion.Euler(pitch, player.transform.eulerAngles.y, 0f);
        }
    }
}
