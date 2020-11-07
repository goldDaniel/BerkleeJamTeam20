using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatCollision : MonoBehaviour
{
    [SerializeField]
    public CameraController camController; 
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            camController.targetOrthoSize = 6;
            camController.targetPosition = this.transform;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            camController.targetOrthoSize = 12;
            camController.targetPosition = collision.gameObject.transform;
        }
    }
}
