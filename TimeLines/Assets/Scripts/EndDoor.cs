using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDoor : MonoBehaviour {

    public GameManager gameManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        col.gameObject.GetComponent<CharacterBehaviour>().FadePlayer();
        if (!gameManager.EndReached && col.gameObject.tag == "AlternatePlayer")
        {
            gameManager.EndReached = true;
            gameManager.PlayerWon = false;
        }
        if (!gameManager.EndReached && col.gameObject.tag == "Player")
        {
            gameManager.EndReached = true;
            gameManager.PlayerWon = true;
            
        }
    }
}
