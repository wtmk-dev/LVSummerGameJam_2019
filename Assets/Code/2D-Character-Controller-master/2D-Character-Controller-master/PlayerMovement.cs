using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController2D controller2D;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody2D;
    private float horizonatalMove = 0f;
    [SerializeField]
    private float runSpeed = 40f;
    private bool willJump = false;
    private bool willCrouch = false;
    private bool isMoving = false;
    private bool isActive = false;
    private Animator animator;
    private PlayerModel playerModel;


    void Awake()
    {
        controller2D = GetComponent<CharacterController2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        isActive = true;
    }

    void Update()
    {
        if(isActive)
        {
            horizonatalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        }

        if(horizonatalMove != 0)
        {
            isMoving = true;
        }else
        {
            isMoving = false;
        }

        if( Input.GetButtonDown("Jump") && playerModel.Level > 0 && isActive)
        {   
            willJump = true;
        }

        if( Input.GetButtonDown("Crouch") )
        {
            willCrouch = true;
        }else if( Input.GetButtonUp("Crouch") )
        {
            willCrouch = false;
        }

        UpdateAnimationState();
    }

    void FixedUpdate()
    {
       controller2D.Move(horizonatalMove * Time.fixedDeltaTime, willCrouch, willJump);
       willJump = false;
       animator.SetBool("IsWalking",isMoving);
    }

    public void Init(PlayerModel playerModel)
    {
        this.playerModel = playerModel;
    }

    public void UpdateAnimationState()
    {
        animator.SetBool("IsJumping", willJump);
        animator.SetInteger("Level",playerModel.Level);
        animator.SetBool("hasWon", playerModel.hasWon);
        animator.SetBool("hasLost", playerModel.hasLost);
    }

    public void LevelUp()
    {
        runSpeed += 20f;

        if(playerModel.hasWon)
        {
            isActive = false;
            transform.position = new Vector3(0f,0f,0f);
            transform.localScale = new Vector3(2f,2f,2f);
            isActive = true;
            runSpeed = 100f;
            rigidbody2D.gravityScale = 0.2f;
        }
    }

    public void LevelDown()
    {
        runSpeed -= 10f;
    }

}   
