// Summary:
//
// Created by: Julian Glass-Pilon
//
// (C)2016 TwinSoft Studios

using UnityEngine;
using System.Collections;

public class AlternateTimelineTrigger : MonoBehaviour 
{
    private GameManager gameManager;
    private Transform target;
    private GameObject[] siblingTriggers;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        target = transform.GetChild(0);
        siblingTriggers = new GameObject[transform.parent.childCount];
        for(int i = 0; i < siblingTriggers.Length; i++)
        {
            siblingTriggers[i] = transform.parent.GetChild(i).gameObject;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player" || other.transform.tag == "AlternatePlayer")
        {
            gameManager.CreateAlternateTimeline(target);
            foreach (GameObject trigger in siblingTriggers)
            {
                trigger.GetComponent<Collider>().enabled = false;
            }
        }
    }
}
