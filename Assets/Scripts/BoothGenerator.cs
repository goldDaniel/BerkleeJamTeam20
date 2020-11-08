using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoothGenerator : MonoBehaviour
{

    public GameObject boothPrefab;
    public CameraController controller;
    public PlayerController player;

    public int boothCount = 4;

    public List<Sprite> boothSprites;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < boothCount; i++)
        {
            GameObject booth = Instantiate(boothPrefab);
            booth.transform.parent = this.transform;

            booth.GetComponentInChildren<ChatCollision>().camController = controller;
            booth.GetComponentInChildren<ChatCollision>().player = player;


            Vector3 pos = new Vector3();
            pos.x = Random.Range(-20, 20);
            pos.y = Random.Range(-20, 20);
            booth.transform.position = pos;

            int spriteIndex = Random.Range(0, boothSprites.Count);
            booth.GetComponent<SpriteRenderer>().sprite = boothSprites[spriteIndex];
            boothSprites.RemoveAt(spriteIndex);
        }
    }
}
