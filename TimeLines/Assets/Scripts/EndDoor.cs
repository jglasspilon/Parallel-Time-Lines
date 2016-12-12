using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDoor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision other)
    {
        other.gameObject.GetComponent<CharacterBehaviour>().KillPlayer();
    }

    void OnTriggerEnter(Collider col)
    {
        col.gameObject.GetComponent<CharacterBehaviour>().KillPlayer();
    }
}
