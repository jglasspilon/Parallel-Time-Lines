using UnityEngine;
using System.Collections;

public class CharacterBehaviour : MonoBehaviour {
    //public variables to affect the jump strength, move strength and max move speed
    public float jumpForce = 1000f;
    public float moveForce = 200f;
    public float maxSpeed = 500f;

    //public object checks for contact with ground
    public Transform groundCheck;
    public Transform topCheck;
    public Transform bottomCheck;

    //checks the jumping, ground contact and direction the character is facing
    protected bool isJumping = false;
    protected bool grounded = false;
    protected bool facingRight = true;
    protected bool canMove = true;
    protected bool sideContact = false;

    //references of components
    protected Rigidbody characterRB;

    // Use this for initialization
    void Start()
    {
        //get the rigidbody2D component from the character
        characterRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //checks if a line hits the ground layer between the character and the groundCheck object 
        grounded = Physics.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        sideContact = (Physics.Linecast(topCheck.position, bottomCheck.position, 1 << LayerMask.NameToLayer("Ground")));
    }

    // Update is called at a fixed frame rate regardless of comp speed
    protected virtual void FixedUpdate()
    {
        if (!sideContact)
        {
            //move the character by a force proportional to the axis input strength
            characterRB.AddForce(Vector2.right * moveForce);
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
