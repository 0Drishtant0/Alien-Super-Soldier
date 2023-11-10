using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterLocomotion : MonoBehaviour
{
    [SerializeField] private float jumpHeight;
    [SerializeField] private float gravity;
    [SerializeField] private float stepDown;
    [SerializeField] private float airControl;
    [SerializeField] private float jumpDamp;
    [SerializeField] private float jumpTimer;
    [SerializeField] private float groundSpeed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    [SerializeField] private float attackCoolDownTime;
    [SerializeField] private CharacterAiming ca;

    public static int noOfClicks = 0;
    private float lastClickedTime = 0;
    private float maxComboDelay = 1;
    private float nextFireTime = 0f;

    private bool currentlyAttacking = false;

    private Animator animator;
    CharacterController cc;
    private Vector2 input;

    private Vector3 rootMotion;
    private Vector3 velocity;
    private bool isJumping;
    private bool drawWeapon;
  
   

    private int isSprintingParam = Animator.StringToHash("isSprinting");

    private PlayerControls playerActions;

    void Start()
    {
        animator = GetComponent<Animator>();
        cc  = GetComponent<CharacterController>();
        playerActions = new PlayerControls();
        playerActions.Player.Enable();
        ca = GetComponent<CharacterAiming>();
    }

    // Update is called once per frame
    void Update()
    {
        if (drawWeapon)
        {
            ca.canShoot = false;
        }
        else
        {
            ca.canShoot = true;
        }
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        animator.SetFloat("InputX", input.x);
        animator.SetFloat("InputY", input.y);

        UpdateIsSprinting();
       
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {   
           
            animator.SetBool("isRolling", true);
        }
        else
        {   
         
            animator.SetBool("isRolling", false);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetBool("drawWeapon", !drawWeapon);
            drawWeapon = !drawWeapon;
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Q) && drawWeapon)
        {
            animator.SetBool("upAttack", true);
        }
        else
        {
            animator.SetBool("upAttack", false);
        }

        if (Input.GetKeyDown(KeyCode.E) && Input.GetKey(KeyCode.LeftShift) && !currentlyAttacking)
        {
            animator.SetBool("dashAttack", true);
        }
        else
        {
            animator.SetBool("dashAttack", false);
        }

        // Attack animations
        if (animator.GetCurrentAnimatorStateInfo(1).normalizedTime > 0.3f && animator.GetCurrentAnimatorStateInfo(1).IsName("Attack_3Combo_1_Inplace"))
        {
            animator.SetBool("attack1", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(1).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(1).IsName("Jump_Attack_Combo_6_Attach"))
        {
            animator.SetBool("attack1", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(1).normalizedTime > 0.3f && animator.GetCurrentAnimatorStateInfo(1).IsName("Attack_3Combo_2_Inplace"))
        {
            animator.SetBool("attack2", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(1).normalizedTime > 0.3f && animator.GetCurrentAnimatorStateInfo(1).IsName("Attack_3Combo_3_Inplace"))
        {
            animator.SetBool("attack3", false);
        }

        if (animator.GetCurrentAnimatorStateInfo(1).normalizedTime > 0.3f && animator.GetCurrentAnimatorStateInfo(1).IsName("Attack_4Combo_1"))
        {
            animator.SetBool("heavyAttack1", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(1).normalizedTime > 0.3f && animator.GetCurrentAnimatorStateInfo(1).IsName("Attack_4Combo_2"))
        {
            animator.SetBool("heavyAttack2", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(1).normalizedTime > 0.3f && animator.GetCurrentAnimatorStateInfo(1).IsName("Attack_4Combo_3"))
        {
            animator.SetBool("heavyAttack3", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(1).normalizedTime > 0.3f && animator.GetCurrentAnimatorStateInfo(1).IsName("Attack_4Combo_4"))
        {
            animator.SetBool("heavyAttack4", false);
        }

        if (Time.time - lastClickedTime > maxComboDelay)
        {
            noOfClicks = 0;
        }
        if (Time.time > nextFireTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && drawWeapon)
            {
                OnClick();
            }

            if (Input.GetKeyDown(KeyCode.Mouse1) && drawWeapon)
            {
                OnRightClick();
            }
        }
        // end
    }

    private void UpdateIsSprinting()
    {
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        animator.SetBool(isSprintingParam, isSprinting);

    }

    private void OnAnimatorMove()
    {
        rootMotion += animator.deltaPosition;
    }

    private void FixedUpdate()
    {
        if (isJumping) // IsInAir State
        {
            UpdateInAir();
        }
        else // IsOnGround state
        {
            UpdateOnGround();
        }
    }

    private void UpdateOnGround()
    {
        cc.Move(rootMotion * groundSpeed + Vector3.down * stepDown);
        rootMotion = Vector3.zero;

        if (!cc.isGrounded)
        {
            isJumping = true;
            velocity = animator.velocity * jumpDamp;
            velocity.y = 0;
        }
    }

    private void UpdateInAir()
    {
        velocity.y -= gravity * Time.fixedDeltaTime;
        Vector3 displacement = velocity * Time.fixedDeltaTime;
        displacement += CalculateAirControl();
        cc.Move(displacement);
        isJumping = !cc.isGrounded;
        rootMotion = Vector3.zero;
        animator.SetBool("isJumping", isJumping);
    }

    Vector3 CalculateAirControl()
    {
        return ((transform.forward * input.y) + (transform.right * input.x)) * (airControl / 100);
    }

    private void Jump()
    {
        if (!isJumping)
        {
            isJumping = true;
            velocity = animator.velocity * jumpDamp * groundSpeed;
            velocity.y = Mathf.Sqrt(2 * gravity * jumpHeight);
            animator.SetBool("isJumping", true);
        }
    }

    private void OnClick()
    {
        lastClickedTime = Time.time;
        noOfClicks++;
        if (noOfClicks == 1)
        {
            animator.SetBool("attack1", true);
        }
        noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);

      //  Debug.Log(noOfClicks);

        if (noOfClicks >= 2 && animator.GetCurrentAnimatorStateInfo(1).normalizedTime > 0.3f && animator.GetCurrentAnimatorStateInfo(1).IsName("Attack_3Combo_1_Inplace"))
        {
            animator.SetBool("attack1", false);
            animator.SetBool("attack2", true);
        }

        if (noOfClicks >= 3 && animator.GetCurrentAnimatorStateInfo(1).normalizedTime > 0.3f && animator.GetCurrentAnimatorStateInfo(1).IsName("Attack_3Combo_2_Inplace"))
        {
            animator.SetBool("attack2", false);
            animator.SetBool("attack3", true);
        }
    }

    private void OnRightClick()
    {
        lastClickedTime = Time.time;
        noOfClicks++;
        if (noOfClicks == 1)
        {
            animator.SetBool("heavyAttack1", true);
        }
        noOfClicks = Mathf.Clamp(noOfClicks, 0, 4);

       // Debug.Log(noOfClicks);

        if (noOfClicks >= 2 && animator.GetCurrentAnimatorStateInfo(1).normalizedTime > 0.3f && animator.GetCurrentAnimatorStateInfo(1).IsName("Attack_4Combo_1"))
        {
            animator.SetBool("heavyAttack1", false);
            animator.SetBool("heavyAttack2", true);
        }

        if (noOfClicks >= 3 && animator.GetCurrentAnimatorStateInfo(1).normalizedTime > 0.3f && animator.GetCurrentAnimatorStateInfo(1).IsName("Attack_4Combo_2"))
        {
            animator.SetBool("heavyAttack2", false);
            animator.SetBool("heavyAttack3", true);
        }

        if (noOfClicks >= 4 && animator.GetCurrentAnimatorStateInfo(1).normalizedTime > 0.3f && animator.GetCurrentAnimatorStateInfo(1).IsName("Attack_4Combo_3"))
        {
            animator.SetBool("heavyAttack3", false);
            animator.SetBool("heavyAttack4", true);
        }
    }
}
