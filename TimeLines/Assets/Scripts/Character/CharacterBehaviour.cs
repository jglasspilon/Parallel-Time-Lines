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
    public Camera activeCam;
    public AudioClip jumpSound;
    public AudioClip landSound;

    //checks the jumping, ground contact and direction the character is facing
    protected bool isJumping = false;
    protected bool grounded = true;
    protected bool facingRight = true;
    protected bool canMove = true;
    protected bool sideContact = false;

    //references of components
    protected Rigidbody characterRB;

    //for death
    private bool isDead = false;
    private bool goingUp = false;

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
        var previousGrounded = grounded;
        grounded = Physics.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        sideContact = (Physics.Linecast(topCheck.position, bottomCheck.position, 1 << LayerMask.NameToLayer("Ground")));

        if(!previousGrounded && grounded)
            GetComponent<AudioSource>().PlayOneShot(landSound, 1);

        //for the death animation
        Vector3 topPosition = transform.position + (Vector3.up * 0.5f);
        Vector3 deathRotation = transform.rotation.eulerAngles + new Vector3(0, 0, 2);

        if (goingUp)
        {
            transform.position = Vector3.Lerp(transform.position, topPosition, 0.6f);
            transform.rotation = Quaternion.Euler(Vector3.Lerp(transform.rotation.eulerAngles, deathRotation, 1));
        }
    }

    // Update is called at a fixed frame rate regardless of comp speed
    protected virtual void FixedUpdate()
    {
        if (!isDead)
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
                GetComponent<AudioSource>().PlayOneShot(jumpSound, 1);
                characterRB.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
                isJumping = false;
            }
        }
    }

    public void KillPlayer()
    {
        if (!isDead)
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Collider>().enabled = false;
            StartCoroutine(DeathAnimation());
            isDead = true;

            activeCam.GetComponent<CameraFollowUnit>().enabled = false;
        }
    }

    private IEnumerator DeathAnimation()
    {
        goingUp = true;
        yield return new WaitForSeconds(0.1f);

        goingUp = false;
        GetComponent<Rigidbody>().useGravity = true;
        yield return new WaitForSeconds(1);

        activeCam.GetComponent<CameraFollowUnit>().enabled = false;
        Destroy(gameObject);
    }
}
