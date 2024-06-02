using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    public float runSpeed = 7f;
    public float rotationSpeed = 250f;
    public Animator animator;

    private float x, y;

    public float alturaSalto = 3f;
    public float gravedad = -9.81f;
    private Vector3 velocidad;

    public Transform checkPiso;
    private float distanciaPiso = 0.4f;
    public LayerMask maskPiso;
    private bool enPiso;

    public float mouseSensitivity = 100f;
    public Camera camaraPersonaje;
    public CharacterController characterController;

    private float yaw = 0f;
    private float pitch = 0f;
    public float pitchMin = -30f;
    public float pitchMax = 60f;

    private SoundManager soundManager;
    private AudioSource audioSource;

    // Variables para sonidos de pasos
    public int footstepAudioIndex = 3; // Índice del sonido de paso en el SoundManager
    public float footstepDelay = 0.5f; // Retraso entre sonidos de pasos
    private float footstepTimer;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Inicializa el SoundManager
        soundManager = FindObjectOfType<SoundManager>();
        if (soundManager == null)
        {
            throw new System.Exception("SoundManager not found in the scene.");
        }

        yaw = transform.eulerAngles.y;
        pitch = 0f;
        footstepTimer = footstepDelay; // Inicializar el temporizador

        Cursor.lockState = CursorLockMode.Locked; // Ocultar y bloquear el cursor en el centro de la pantalla
        Cursor.visible = false;
    }

    void Update()
    {
        MovimientoDeCamara();
        MovimientoDelPersonaje();

        // Animaciones
        animator.SetFloat("velx", x);
        animator.SetFloat("vely", y);

        if (Input.GetKey("j"))
        {
            animator.SetBool("otro", false);
            animator.Play("dance");
        }
        if (x != 0 || y != 0)
        {
            animator.SetBool("otro", true);
            HandleFootsteps(); // Gestionar los sonidos de pasos
        }
        else
        {
            animator.SetBool("otro", false);
        }

        // Verificar si está en el suelo
        enPiso = Physics.CheckSphere(checkPiso.position, distanciaPiso, maskPiso);

        if (enPiso && velocidad.y < 0)
        {
            velocidad.y = -2f;
        }

        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && enPiso)
        {
            animator.Play("jump");
            velocidad.y = Mathf.Sqrt(alturaSalto * -2f * gravedad);
        }

        // Aplicar gravedad
        velocidad.y += gravedad * Time.deltaTime;
        characterController.Move(velocidad * Time.deltaTime);
    }

    void MovimientoDelPersonaje()
    {
        // Obtener entrada del teclado
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        // Mover el personaje
        Vector3 movement = transform.right * x + transform.forward * y; // Cálculo del vector de movimiento
        characterController.Move(movement * runSpeed * Time.deltaTime); // Aplicar movimiento
    }

    void MovimientoDeCamara()
    {
        // Movimiento de la cámara en base al movimiento del mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);

        // Rotación del jugador
        transform.localRotation = Quaternion.Euler(0f, yaw, 0f);

        // Rotación de la cámara
        camaraPersonaje.transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    private void HandleFootsteps()
    {
        // Reducir el temporizador
        footstepTimer -= Time.deltaTime;

        // Si el temporizador ha terminado, reproducir un sonido de paso
        if (footstepTimer <= 0f)
        {
            if (soundManager != null && audioSource != null)
            {
                soundManager.SelectAudio(audioSource, footstepAudioIndex, 0.01f); // Llamar al SoundManager para reproducir el sonido
            }

            // Reiniciar el temporizador
            footstepTimer = footstepDelay;
        }
    }
}
