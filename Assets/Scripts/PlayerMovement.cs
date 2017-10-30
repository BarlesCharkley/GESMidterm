using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    #region Editor Fields

    [SerializeField]
    private float speed = 8;

    [SerializeField]
    private float jumpHeight = 6;

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

    [SerializeField]
    private PhysicsMaterial2D defaultPhysics;

    [SerializeField]
    private PhysicsMaterial2D powerUpPhysics;

    #endregion

    # region Private Fields

    private bool isOnGround;

    private Rigidbody2D myRigidbody2D;

    private Collider2D myCollider;

    private Animator myAnimator;

    private float horizontalInput;

    private bool pressedJump;

    public int coinCount;

    #endregion


    private void UpdateIsOnGround()
    {
        Collider2D[] groundColliders =
            Physics2D.OverlapCircleAll(groundCheckPosition.position, groundCheckRadius, groundLayer);

        isOnGround = groundColliders.Length > 0;
    }

    // Use this for initialization
    void Start ()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        myAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        GetMovementInput();
        GetJumpInput();
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

            isOnGround = false;
        }
    }

    private void FallDeath()
    {
        if (GetComponent<Transform>().position.y <= deathHeight)
        {
            Debug.Log("Casul");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void GivePowerup()
    {
        StartCoroutine(PowerUp());
    }

    IEnumerator PowerUp()
    {
        speed += powerupStrength;
        myCollider.sharedMaterial = powerUpPhysics;
        myRigidbody2D.sharedMaterial = powerUpPhysics;

        yield return new WaitForSeconds(powerupTime);

        speed -= powerupStrength;
        myCollider.sharedMaterial = defaultPhysics;
        myRigidbody2D.sharedMaterial = defaultPhysics;
        
    }
}
