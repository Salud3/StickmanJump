using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask ContactoInferior;
    [SerializeField] private float mv;
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

    [Header("Dashed")]
    [SerializeField] private bool DashUsable;
    [SerializeField] private bool dashed;

    [Header("Walls")]
    [SerializeField] private bool inWall;
    [SerializeField] private float WallSlideSpeed;
    [SerializeField] private bool OnSlide;
    private float MaxWallSlideSpeed;

    [SerializeField] private Vector3 wallJumpForce;
    [SerializeField] private float wallJumpDuration;
    [SerializeField] private bool OnWallJump;


    private void Awake()
    {
        rg = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        StartCoroutine(DashedUsable());
        StartCoroutine(JumpOnWall());

    }
    void Update()
    {
        if (!inWall)
        {
            DetectDash();
        }

        DetectJump();
        
    }
    private void FixedUpdate()
    {
        mv = Input.GetAxis("Horizontal");
        
        //print("Contacto: " + GroundCheck());
        if (!coyoteAct)
        {
            coyoteTim -= Time.deltaTime;
        }

        DetectCollisions();
        
        AnimatorLogic();

        GravityCheck();

        if (OnWallJump)
        {
            jumpwall();
        }

        PlayerMove(mv);
        
        

      
       
    }

    bool left;
    bool right = true;
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
        StopCoroutine(StartSlide());

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

    void GravityCheck()
    {
        float gravity1 = 1f;

        if (inWall)
        {
            //canMove = false;
            if (!OnSlide)
            {
                rg.velocity = Vector3.zero; rg.velocity.Normalize();
                StartCoroutine(StartSlide());
            }
            else
            {
                MaxWallSlideSpeed = WallSlideSpeed + 5;
                //Testear sin estar en movimiento
                if (mv != 0) {
                    rg.velocity = new Vector3(rg.velocity.x, Mathf.Clamp(rg.velocity.y, -WallSlideSpeed, MaxWallSlideSpeed));
                }
            }
        }
        else if (!inWall)
        {
            //canMove = true;
            gravity = gravity1;
        }

        rg.AddForce(gravity * Vector3.down, ForceMode.Impulse);

    }
    IEnumerator StartSlide()
    {

        yield return new WaitForSeconds(2f);
        if (!inWall)
        {
            yield break;
        }
        else
        {
            OnSlide = true;
        }
    }

    void AnimatorLogic()
    {
        if (left)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (right)
        {
            GetComponent<SpriteRenderer>().flipX = false;

        }

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
            animator.SetTrigger("Jump");
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
        StopCoroutine(StartSlide());
        OnSlide = false;
        //canMove = true;

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
            StopCoroutine(StartSlide());


        }
        else if (wall && !doublejump)
        {
            print("inwall jump");

            OnWallJump = true;
            Invoke("wallfalse", wallJumpDuration);

        }
        else if (wall && doublejump)
        {
            print("inwall jump2");
            OnWallJump = true;
            Invoke("wallfalse", wallJumpDuration);
        }
    }


    public bool jumpwallusable;
    void jumpwall()
    {
        if (jumpwallusable){ 
        rg.velocity = new Vector3(-mv * wallJumpForce.x, wallJumpForce.y);
        Invoke("jumponwal", 0.1f);
        }
        else
        {
            Debug.Log("jumpwall no disponible");
        }
        
    }

    void jumponwal()
    {
        jumpwallusable = false;
        inWall = false;
        rg.velocity = new Vector3(mv * wallJumpForce.x, wallJumpForce.y/2);
        StartCoroutine(JumpOnWall());
    }

    IEnumerator JumpOnWall()
    {
        yield return new WaitForSeconds(.15f);
        if (!inWall)
        {
            rg.velocity = new Vector3(mv, gravity);
        }
        yield return new WaitForSeconds(.35f);
        jumpwallusable = true;
    }

    void wallfalse()
    {
        OnWallJump = false;
    }
    //Controlador del movimiento
    void PlayerMove(float mv)
    {

        movement.Set(mv, 0f, 0f);

        if (mv <0)
        {
            left = true;
            right = false;
        }
        else if(mv>0)
        {
            left = false;
            right = true;
        }

        animator.SetBool("Walk", mv !=0 && isGround);
        
        movement = movement.normalized * Speed * Time.deltaTime;

        rg.MovePosition(transform.position + movement);
        
    }

    //Deteccion de Colisiones
    void DetectCollisions()
    {

        float rayLegthD;
        float rayLegthM = 0.3f;
        float rayLegth = .7f;

        if (!jumpwallusable)
        {
            rayLegthD = 1.5f;
        }
        else
        {
            rayLegthD = 0.8f;
        }

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
            OnSlide = false;

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

            //Deteccion de paredes
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
                OnSlide = false;
            }
            else if (WallLEFT && FLOOR)
            {
                inWall = false;
                isfall = false;
                isGround = true;
                doubleJump = true;
                coyoteAct = true;
                OnSlide = false;
            }
            else
            {
                isfall = true;
                inWall = false;
                isGround = false;
                coyoteAct = false;
                OnSlide = false;
            }

        }




    }


}
