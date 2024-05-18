using System.Collections;
using System.Collections.Generic;
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

  void Start()
  {
    yaw = transform.eulerAngles.y;
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
}
