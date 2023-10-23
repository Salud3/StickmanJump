using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject personaje;
    public float Speed = 5.0f;
    public float runSpeed = 10.0f;
    public float jumpForce = 20f;
    public float doublejumpForce = 15f;
    public float gravity = 6.2f; // Gravedad al caer
    public bool isdash= false;
    public bool dashUsable= true;

    private Rigidbody rg;
    private Animator animator;
    private bool isGrounded = true;
    private bool isRunning = false;
    public bool doubleJump = false;
    public bool isDoubleJump = false;
    public bool izquierda = false;
    public bool derecha = false;
    public int dashNum = 0;
    public bool paredDerecha = false;
    public bool paredIzquierda = false;
    public float saltoDePared = 0;
    public float saltoDePared2 = 0;

    void Start()
    {
        rg = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(0, 0, horizontalInput);

        // Detectar si el personaje está caminando o corriendo
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movement *= runSpeed;
            isRunning = true;
        }
        else 
        {
            movement *= Speed;
            isRunning = false;
        }

        if (Input.GetKey(KeyCode.Mouse1) && dashUsable == true)
        {
            isdash = true;
            if (derecha ==true)
            {
                dashNum = 35; 
            }
            else if (izquierda == true)
            {
                dashNum = -35   ;
            }
            //movement *= 50;
          
            Invoke("DesactivarDash", 0.3f);            
        } 
     
        rg.velocity = new Vector3(movement.x, rg.velocity.y + saltoDePared2, movement.z+dashNum + saltoDePared);

        // Rotación del personaje
        if (horizontalInput < 0)
        {
           transform.rotation = Quaternion.Euler(0, -180, 0);
            izquierda = true;
            derecha = false;       
        }
        else if (horizontalInput > 0)
        {
           transform.rotation = Quaternion.Euler(0, 0, 0);
            izquierda = false;
            derecha = true;
        }

        // Configurar las animaciones
        animator.SetBool("IsWalking", movement.magnitude > 0);
        animator.SetBool("IsRunning", isRunning);
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetBool("IsDash", isdash);

        // Control de la gravedad
        if (!isGrounded && paredDerecha == false && paredIzquierda==false)
        {
            rg.AddForce(Vector3.down * gravity);
        }

        if (isGrounded && Input.GetButtonDown("Jump") && paredIzquierda == false && paredDerecha == false)
        {
            rg.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            doubleJump = true;
            isDoubleJump = false;
        }
        else if (isGrounded == false && doubleJump== true && Input.GetButtonDown("Jump") && paredIzquierda == false && paredDerecha == false)
        {
            isGrounded = false;
            doubleJump = false;
            isDoubleJump = true;
            Invoke("CaidaDobleSalto", 0.9f);
            rg.AddForce(Vector3.up * doublejumpForce, ForceMode.Impulse);      

        }

        if (Input.GetButtonDown("Jump") && paredDerecha==true)
        {
          //  saltoDePared2 = 0.4f;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            paredDerecha = false;
            saltoDePared = 20;         
            rg.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;         
            Invoke("DesactivarSaltoPared", 0.3f);
        }
        else if(Input.GetButtonDown("Jump") && paredIzquierda == true)
        {
           // saltoDePared2 = 0.4f;
            transform.rotation = Quaternion.Euler(0, 180, 0);
            paredIzquierda = false;
            saltoDePared = -20;
            rg.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            // rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            Invoke("DesactivarSaltoPared", 0.3f);
        }
    }

    public void DesactivarSaltoPared()
    {

        saltoDePared = 0;
        saltoDePared2 = 0;
    }

    public void DesactivarDash()
    {
        dashUsable = false;
        isdash = false;
        dashNum = 0;
        Invoke("ActivarDash", 2f);

    }

    public void ActivarDash()
    {
        dashUsable = true;   
    }
    public void CaidaDobleSalto() 
    {
        isDoubleJump = false;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isDoubleJump = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DER"))
        {
            paredDerecha = true;
            isGrounded = false;
        }
        if (other.gameObject.CompareTag("IZQ"))
        {
            paredIzquierda = true;
            isGrounded = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("DER"))
        {
            paredDerecha = false;
        }
        if (other.gameObject.CompareTag("IZQ"))
        {
            paredIzquierda = false;
        }
    }
}
