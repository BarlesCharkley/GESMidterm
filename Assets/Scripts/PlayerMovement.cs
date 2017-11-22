using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    #region Editor Fields

    [SerializeField]
    private float speed = 8;

    [SerializeField]
    private float jumpHeight = 6;

    [SerializeField]
    private float slamStrength = 50;

    [SerializeField]
    private Transform groundCheckPosition;

    [SerializeField]
    private float groundCheckRadius = 0.35f;

    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private float deathHeight = -10;

    [SerializeField]
    private float powerupTime;

    [SerializeField]
    private float powerupStrength;

    //[SerializeField]
    //private PhysicsMaterial2D defaultPhysics;

    //[SerializeField]
    //private PhysicsMaterial2D powerUpPhysics;

    [SerializeField]
    private Sprite regularCherry;

    [SerializeField]
    private Sprite powerupCherry;

    #endregion

    # region Private Fields

    private bool isOnGround;

    private Rigidbody2D myRigidbody2D;

    private Collider2D myCollider;

    private Animator myAnimator;

    private SpriteRenderer myRenderer;

    private float horizontalInput;

    private bool pressedJump;

    private bool pressedSlam;

    public int coinCount;

    private float defaultSpeed;

    private Hazard hazardScript;

    #endregion


    private void UpdateIsOnGround()
    {
        Collider2D[] groundColliders =
            Physics2D.OverlapCircleAll(groundCheckPosition.position, groundCheckRadius, groundLayer);

        isOnGround = groundColliders.Length > 0;
    }

    void Start ()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        myAnimator = GetComponentInChildren<Animator>();
        myRenderer = GetComponentInChildren<SpriteRenderer>();
        hazardScript = FindObjectOfType<Hazard>();
        defaultSpeed = speed;
	}
	
	void Update ()
    {
        GetMovementInput();
        GetJumpInput();
        CheckForPowerup();

        if (myRenderer.enabled == false)
        {
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        HandlePlayerMovement();
        UpdateIsOnGround();
        HandleJump();
        FallDeath();
    }

    private void GetMovementInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void GetJumpInput()
    {
        pressedJump = Input.GetButtonDown("Jump");
        pressedSlam = Input.GetKeyDown(KeyCode.S);
    }

    private void HandlePlayerMovement()
    {
        myRigidbody2D.velocity =
            new Vector2(speed * horizontalInput, myRigidbody2D.velocity.y);

    }

    private void HandleJump()
    {
        if (pressedJump && isOnGround)
        {
            myRigidbody2D.velocity =
                new Vector2(myRigidbody2D.velocity.x, jumpHeight);

            myAnimator.SetTrigger("Jump");

            //isOnGround = false;
        }

        if (pressedSlam && !isOnGround)
        {
            myRigidbody2D.velocity = new Vector2(0, 0);
            myRigidbody2D.AddForce(new Vector2(0, -slamStrength), ForceMode2D.Impulse);
        }
    }

    private void FallDeath()
    {
        if (GetComponent<Transform>().position.y <= deathHeight)
        {
            hazardScript.Die();
        }
    }

    public void GivePowerup()
    {
        StartCoroutine(PowerUp());
    }

    IEnumerator PowerUp()
    {
        speed += powerupStrength;
        jumpHeight += (powerupStrength / 2);
        yield return new WaitForSeconds(powerupTime);
        speed -= powerupStrength;
        jumpHeight -= (powerupStrength / 2);
    }

    void CheckForPowerup()
    {
        if (speed == defaultSpeed)
        {
            //myRigidbody2D.sharedMaterial = defaultPhysics;
            //myCollider.sharedMaterial = defaultPhysics;
            myRenderer.sprite = regularCherry;
        }

        else
        {
            //myRigidbody2D.sharedMaterial = powerUpPhysics;
            //myCollider.sharedMaterial = powerUpPhysics;
            myRenderer.sprite = powerupCherry;
        }
    }

}
