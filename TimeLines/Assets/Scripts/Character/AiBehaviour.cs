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
        var shouldJump = Random.Range(0, 2) == 0;

        if (col.gameObject.tag == "DecisionPoint" && grounded && shouldJump)
        {
            characterRB.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
        }
    }
}
