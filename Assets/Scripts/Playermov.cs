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
    
    public Rigidbody rb;
    public float alturaSalto = 3f;

    public Transform checkPiso;
    private float distanciaPiso = 0.1f;
    public LayerMask maskPiso;
    bool enPiso;

    public float mouseSensitivity = 100f;

    private float yaw = 0f;
  
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
        footstepTimer = footstepDelay; // Inicializar el temporizador
    }
    
    void Update()
    {
        // Rotación basada en el mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        yaw += mouseX;
        transform.rotation = Quaternion.Euler(0f, yaw, 0f);
        
        // Obtener entrada del teclado
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        // Mover el personaje
        Vector3 movement = transform.right * x + transform.forward * y; // Cálculo del vector de movimiento
        transform.position += movement * runSpeed * Time.deltaTime; // Aplicar movimiento

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

        // Salto
        enPiso = Physics.CheckSphere(checkPiso.position, distanciaPiso, maskPiso);
        if (Input.GetKey("space") && enPiso)
        {
            animator.Play("jump");
            Invoke("Jump", 0.001f);
        }
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * alturaSalto, ForceMode.Impulse);
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
