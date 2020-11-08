﻿
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Vector3 velocity;
    public Rigidbody2D body;

    private Booth booth;
    public Inventory inventory;


    // Start is called before the first frame update
    void Start()
    {
        booth = null;
        body = GetComponent<Rigidbody2D>();
    }

    public void SetBooth(Booth booth)
    {
        this.booth = booth;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.T) && booth != null)
        {
            if (inventory.currItem != booth.buying)
            {
                Debug.Log("invalid trade!");
            }
            else
            {
                inventory.tradeItem(0);
            }
        }
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

    }
}
