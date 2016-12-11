using UnityEngine;
using System.Collections;

public class AiBehaviour : CharacterBehaviour {

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "DecisionPoint")
        {
            var shouldJump = Random.Range(0, 2) == 0;
            isJumping = shouldJump && grounded;
        }
        else if(col.gameObject.tag == "Trap")
        {
            //10% of the time, it will die
            var shouldAvoidTrap = Random.Range(0, 8) != 0;
            isJumping = shouldAvoidTrap;
        }
    }
}
