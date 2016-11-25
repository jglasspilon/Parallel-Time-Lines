// Summary: controls the player behaviours and interactions


using UnityEngine;
using System.Collections;

public class PlayerBehaviour : CharacterBehaviour
{

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        //if the jump button is pressed and the character is on the ground JUMP!
        if (Input.GetButtonDown("Jump") && grounded)
        {
            isJumping = true;
        }
    }
}
