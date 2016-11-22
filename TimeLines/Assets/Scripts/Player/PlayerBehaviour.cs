﻿// Summary: controls the player behaviours and interactions


using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour 
{
    //public variables to affect the jump strength, move strength and max move speed
    public float jumpForce = 1000f;
    public float moveForce = 200f;
    public float maxSpeed = 500f;

    //public object checks for contact with ground
    public Transform groundCheck;
    public Transform topCheck;
    public Transform bottomCheck;

    //checks the jumping, ground contact and direction the character is facing
    bool isJumping = false;
    bool grounded = false;
    bool facingRight = true;
    bool canMove = true;
    bool sideContact = false;

    //references of components
    Rigidbody characterRB;

    // Use this for initialization
    void Start()
    {

        //get the rigidbody2D component from the character
        characterRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        //checks if a line hits the ground layer between the character and the groundCheck object 
        grounded = Physics.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        sideContact = (Physics.Linecast(topCheck.position, bottomCheck.position, 1 << LayerMask.NameToLayer("Ground")));

        //if the jump button is pressed and the character is on the ground JUMP!
        if (Input.GetButtonDown("Jump") && grounded)
        {
            isJumping = true;
        }
    }

    // Update is called at a fixed frame rate regardless of comp speed
    void FixedUpdate()
    {
        //get the direction of the axis input
        float horizontalStrength = Input.GetAxis("Horizontal");

        //starts the running animation is the character is moving
        if (horizontalStrength != 0 && grounded)
        {
            //walking animation start
        }

        //otherwise brings the animation back to idle
        else {
            //walking animation stop
        }

        if (horizontalStrength == 0)
        {
            //characterRB.velocity = new Vector2(0, characterRB.velocity.y);
        }

        //faces right
        if (horizontalStrength > 0 && !facingRight)
        {
            flip();
        }

        //faces left
        if (horizontalStrength < 0 && facingRight)
        {
            flip();
        }

        if (!sideContact)
        {
            //move the character by a force proportional to the axis input strength
            characterRB.AddForce(Vector2.right * horizontalStrength * moveForce);
            if (Mathf.Abs(characterRB.velocity.x) >= maxSpeed)
            {
                characterRB.velocity = new Vector2(Mathf.Sign(characterRB.velocity.x) * maxSpeed, characterRB.velocity.y);
            }
        }

        //if the character jumps, add the jump force in the up direction
        if (isJumping)
        {
            characterRB.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
            isJumping = false;
        }
    }

    //flips the direction the character is facing
    void flip()
    {
        facingRight = !facingRight;
        Vector3 newScale = transform.localScale;
        newScale.x = -newScale.x;
        transform.localScale = newScale;
    }
}