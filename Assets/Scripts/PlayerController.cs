﻿using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{

    public Vector3 velocity;

    public Rigidbody2D body;

    public bool canTrade;
    public int sellIndex;
    public int buyIndex;
    public Inventory inventory;


    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        //units per seconds
        float speed = 16;

        velocity.Set(0, 0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            velocity.y += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            velocity.y -= 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            velocity.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            velocity.x += 1;
        }

        velocity = velocity.normalized * speed;
        body.velocity = velocity;

        if (Input.GetKey(KeyCode.T) && canTrade)
        {
            if (inventory.currItemIndex != buyIndex)
            {
                Debug.Log("invalid trade!");
            }
            else
            {
                inventory.tradeItem(sellIndex);
            }
        }
    }
}
