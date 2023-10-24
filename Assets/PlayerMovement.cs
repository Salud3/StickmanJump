using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask ContactoInferior;
    [SerializeField] private float Speed = 5;
    [SerializeField] private float gravity = 1f;
    private Rigidbody rg;
    private Vector3 movement;
    private Animator animator;


    //jump and falling
    [Header("Jump and falling")]
    [SerializeField] private float jumpForce = 15;
    [SerializeField] private float coyoteTime = 0.2f;
    [SerializeField] private float jumpBufferTime = 1.5f;

    [SerializeField] private bool isfall;
    [SerializeField] private bool isGround;
    [SerializeField] private bool doubleJump;
    [SerializeField] private bool coyoteAct;
    [SerializeField] private bool jumping;

    private float coyoteTim;
    private float jumpBufferTim;


    [Header("Walls")]
    [SerializeField] private bool inWall;
    [Header("Dashed")]
    [SerializeField] private bool DashUsable;
    [SerializeField] private bool dashed;


    private void Awake()
    {
        rg = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        StartCoroutine(DashedUsable());
    }
    void Update()
    {
        DetectDash();
        DetectJump();
    }

    bool left;
    bool right= true;
    void DetectDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash();
        }
    }

    void Dash()
    {
        float xforce1 = 14.95f;
        float xforce2 = 10.95f;
        if (DashUsable)
        {
            dashed = true;
            if (isGround)
            {
                if (right)
                {
                    rg.AddForce(new Vector3(xforce1, 0), ForceMode.Impulse);
                }
                else if (left)
                {
                    rg.AddForce(new Vector3(-xforce1, 0), ForceMode.Impulse);
                }
            }
            else
            {
                if (right)
                {
                    rg.AddForce(new Vector3(xforce2, 0), ForceMode.Impulse);
                }
                else if (left)
                {
                    rg.AddForce(new Vector3(-xforce2, 0), ForceMode.Impulse);
                }
            }

            
            StartCoroutine(DashedUsable());
        }
    }

    IEnumerator DashedUsable()
    {
        DashUsable = false;
        yield return new WaitForSeconds(.25f);
        dashed = false;
        yield return new WaitForSeconds(2.25f);
        DashUsable = true;
    }
    void DetectJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (coyoteTim > 0f && !inWall)
            {
                jumping = true;
                jumpBufferTim = jumpBufferTime;
                rg.velocity = new Vector3(rg.velocity.x, jumpForce);
                coyoteTim = -1f;
                Jump(mv, false, false);

            }
            else if (coyoteTim > 0f && inWall)
            {
                jumping = true;
                jumpBufferTim = jumpBufferTime;
                rg.velocity = new Vector3(rg.velocity.x, jumpForce);
                coyoteTim = -1f;
                Jump(mv, false, true);

            }
            else if (doubleJump && !inWall)
            {
                doubleJump = false;
                Jump(mv, true, false);

            }
            else if (doubleJump && inWall)
            {
                doubleJump = false;
                Jump(mv, true, true);

            }

        }
        if (jumping && Input.GetKey(KeyCode.Space))
        {
            Jump(mv, false, false);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            jumping = false;

        }
    }
    float mv;
    private void FixedUpdate()
    {
        mv = Input.GetAxis("Horizontal");
        //print("Contacto: " + GroundCheck());
        if (!coyoteAct)
        {
            coyoteTim -= Time.deltaTime;
        }

        if (rg.velocity.y > 0)
        {

        }

        GroundCheck();
        
        AnimatorLogic();

        WallSlide();

        PlayerMove(mv);
      
       
    }

    void WallSlide()
    {
        float gravity1 = 1f;
        float gravity2= .15f;

        if (inWall && !isGround && mv !=0)
        {
            gravity = gravity2;
        }
        else if (isGround)
        {
            gravity = gravity1;
        }


    }

    void AnimatorLogic()
    {
        if (dashed)
        {
            animator.SetBool("Dash", true);
            animator.SetBool("WallSlide", false);
            animator.SetBool("Jump", false);
            animator.SetBool("IsGround", false);
        }else
        if (inWall && !isfall && !isGround && !dashed)
        {
            animator.SetBool("WallSlide", true);
            animator.SetBool("Jump", false);
            animator.SetBool("IsGround", false);
            animator.SetBool("Dash", false);


        }
        else if (isfall && !isGround && !inWall && !dashed)
        {
            animator.SetBool("Jump", true);
            animator.SetBool("IsGround", false);
            animator.SetBool("WallSlide", false);
            animator.SetBool("Dash", false);

        }
        else
        {
            animator.SetBool("IsGround", true);
            animator.SetBool("Jump", false);
            animator.SetBool("Dash", false);
            animator.SetBool("WallSlide", false);
        }

    }


   
    void Jump(float mv, bool doublejump, bool wall)
    {
        //rg.AddForce(jumpForce * Vector3.up,ForceMode.Impulse);

        coyoteTim = -1f;
        if (!doublejump && !wall)
        {

             if (jumpBufferTim > 0)
             {
                 rg.velocity = new Vector3(rg.velocity.x, jumpForce);
                 jumpBufferTim -= Time.deltaTime;

             }
             else if (jumpBufferTim < 0)
             {
                 jumping = false;
             }

        }
        else if (doublejump && !wall)
        {
            rg.velocity += new Vector3(rg.velocity.x, jumpForce*1.25f);
        }
        else if (wall && !doublejump)
        {
            print("inwall jump");
            rg.AddForceAtPosition(new Vector3(-mv * 1.25f, jumpForce * 1.25f),transform.position,ForceMode.Impulse);
        }
        else if (wall && doublejump)
        {
            print("inwall jump");

            rg.AddForceAtPosition(new Vector3(-mv * 1.25f, jumpForce * 1.25f), transform.position, ForceMode.Impulse);
        }

    }

    //Controlador del movimiento
    void PlayerMove(float mv)
    {
        movement.Set(mv, 0f, 0f);

        if (mv <0)
        {
            GetComponent<SpriteRenderer>().flipX = true;

            left = true;
            right = false;
        }
        else if(mv>0)
        {
            GetComponent<SpriteRenderer>().flipX = false;

            left = false;
            right = true;
        }

        animator.SetBool("Walk", mv !=0 && isGround);
        
        movement = movement.normalized * Speed * Time.deltaTime;

        rg.MovePosition(transform.position + movement);
        rg.AddForce(gravity * Vector3.down,ForceMode.Impulse);
        
    }

    //Deteccion de Colisiones
    void GroundCheck()
    {
        float rayLegthD = 0.8f;
        float rayLegthM = 0.3f;
        float rayLegth = .7f;


        Ray ray = new Ray(transform.position - new Vector3(0,.5f), Vector3.down);

        Ray rayDT = new Ray(transform.position+new  Vector3(.4f,.5f,0), Vector3.up);
        Ray rayIT = new Ray(transform.position+new  Vector3(-.4f,.5f,0), Vector3.up);

        Ray raySd = new Ray(transform.position+new  Vector3(.3f,0,0), Vector3.right);
        Ray raySi = new Ray(transform.position+new  Vector3(-.3f,0,0), Vector3.left);

        Debug.DrawRay(ray.origin, ray.direction * rayLegth, Color.yellow);

        Debug.DrawRay(rayIT.origin, rayIT.direction * rayLegthD, Color.yellow);
        Debug.DrawRay(rayDT.origin, rayDT.direction * rayLegthD, Color.yellow);

        Debug.DrawRay(raySd.origin, raySd.direction * rayLegthM, Color.yellow);
        Debug.DrawRay(raySi.origin, raySi.direction * rayLegthM, Color.yellow);

        bool FLOOR = Physics.Raycast(ray.origin, ray.direction, rayLegth, ContactoInferior);

        bool TOPRIGHT = Physics.Raycast(rayDT.origin, rayDT.direction, rayLegthD, ContactoInferior);
        bool TOPLEFT = Physics.Raycast(rayIT.origin, rayIT.direction, rayLegthD, ContactoInferior);


        bool WallRIGHT = Physics.Raycast(raySd.origin, raySd.direction, rayLegthM, ContactoInferior);
        bool WallLEFT = Physics.Raycast(raySi.origin, raySi.direction, rayLegthM, ContactoInferior);

        if (FLOOR && !WallLEFT && !WallRIGHT )
        {
            isfall = false;
            doubleJump = true;
            isGround = true;
            coyoteAct = true;
            inWall = false;

            coyoteTim = coyoteTime;

            return;

            //Deteccion de paredes

        }else
        {
            // Correcion de esquinas
            if (TOPRIGHT && !TOPLEFT)
            {
                Debug.DrawRay(rayDT.origin, rayDT.direction * rayLegthD, Color.red);
                transform.position -= new Vector3(0.4f, 0);
            }
            if (!TOPRIGHT && TOPLEFT)
            {
                transform.position += new Vector3(0.4f, 0);
                Debug.DrawRay(rayIT.origin, rayIT.direction * rayLegthD, Color.red);
            }

            if (WallRIGHT && !FLOOR)
            {
                inWall = true;
                isfall = false;
                isGround = false;
                doubleJump = true;
                coyoteAct = true;

                coyoteTim = coyoteTime;
            }
            else if (WallLEFT && !FLOOR)
            {
                inWall = true;
                isfall = false;
                isGround = false;
                doubleJump = true;
                coyoteAct = true;
            }
            else if (WallRIGHT && FLOOR)
            {
                inWall = false;
                isfall = false;
                isGround = true;
                doubleJump = true;
                coyoteAct = true;
            }
            else if (WallLEFT && FLOOR)
            {
                inWall = false;
                isfall = false;
                isGround = true;
                doubleJump = true;
                coyoteAct = true;
            }
            else
            {
                isfall = true;
                isGround = false;
                coyoteAct = false;
                inWall = false;
            }

        }




    }


}
