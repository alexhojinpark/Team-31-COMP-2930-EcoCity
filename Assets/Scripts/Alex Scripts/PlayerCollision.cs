﻿
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        // Collision Testing Script

        // Debug.Log("hit trigger" + other);
        if (other.tag == "Collider")
        {

            // other.GetComponent<Transform>().position = GetComponentInParent<Transform>().position;
            
        }
    }
}
