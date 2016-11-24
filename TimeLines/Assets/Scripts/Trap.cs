// Summary:
//
// Created by: Julian Glass-Pilon
//
// (C)2016 TwinSoft Studios

using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour 
{
    void OnCollisionEnter(Collision other)
    {
        if(other.transform.tag == "AlternatePlayer")
        {
            Destroy(other.gameObject);
        }

        if(other.transform.tag == "Player")
        {

        }
    }
}
