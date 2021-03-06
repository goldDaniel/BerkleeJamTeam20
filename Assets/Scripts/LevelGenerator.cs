﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelGenerator : MonoBehaviour
{
    public GameObject boothPrefab;
    public GameObject wallPrefab;

    public CameraController controller;
    public PlayerController player;

    public int level = 1;

    private List<Booth> boothsList; 

    public List<Sprite> boothSpritesMaster;

    private float levelSize;
    private float levelTime;
    
    private Item startItem;
    private Item goalItem;


    private float levelOverTime = 4;
    private float levelOverTimer = 4;


    // Start is called before the first frame update
    void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        boothsList = new List<Booth>();

        player.Reset();

        Transform walls = this.transform.Find("Walls");
        Transform booths = this.transform.Find("Booths");

        foreach(Transform child in walls.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in booths.transform)
        {
            Destroy(child.gameObject);
        }

        GenerateSizeAndLevelTime();

        GenerateKiosks();
        GenerateWalls();
    }

    public int GetLevelTime()
    {
        return (int)levelTime;
    }

    private void GenerateSizeAndLevelTime()
    {
        float level1Size = 20;
        levelSize = level1Size + level1Size/2.0f * (level - 1) * 1.01f;

        levelTime = 40 + (level - 1) * 5; //update later with formula
    }

     void Update()
     {
        if(levelTime <= 0)
        {
            levelTime = 0;

            //show lose screen for X 

            levelOverTimer -= Time.deltaTime;
            if(levelOverTimer <= 0)
            {
                levelOverTimer = levelOverTime;
                ResetLevel();
            }
            else
            {
                player.ShowLoseDialog();
            }
        }
        else
        {
            if(player.inventory.currItem == goalItem)
            {
                levelOverTimer -= Time.deltaTime;
                if (levelOverTimer <= 0)
                {
                    levelOverTimer = levelOverTime;
                    level++;
                    GenerateLevel();
                }
                else
                {
                    player.ShowWinDialog();
                }
            }
            else
            {
                levelTime -= Time.deltaTime;
            }
        }
     }

    void ResetLevel()
    {
        player.Reset();
        player.inventory.setupInventory(startItem);
        GenerateSizeAndLevelTime();

        foreach(Booth b in boothsList)
        {
            b.hasPurchased = false;
        }
    }

    private void GenerateKiosks()
    {
        List<Transform> kioskTransforms = new List<Transform>();
        float minDistanceFromKiosk = 15;
        int boothCount = 4 + (2 * (level - 1));

        ItemDatabase db = GameObject.FindGameObjectWithTag("Item Database").GetComponent<ItemDatabase>();
        List<Item> dbList = new List<Item>(db.itemList);
        

        //shufafle dbList
        for (int i = dbList.Count - 1; i >= 1; i--)
        {
            int randomIndex = Random.Range(0,i+1);
            Item temp = dbList[randomIndex];
            dbList[randomIndex] = dbList[i];
            dbList[i] = temp;
        }
        player.inventory.setupInventory(dbList[0]);
        startItem = player.inventory.currItem;



        List<Sprite> boothSprites = new List<Sprite>(boothSpritesMaster);
        for (int i = 0; i < boothCount; i++)
        {
            GameObject booth = Instantiate(boothPrefab);

            Booth boothTyped = booth.GetComponentInParent<Booth>();
            boothsList.Add(boothTyped);

            if (i+1 < dbList.Count)
            {
                boothTyped.buying = dbList[i];
                boothTyped.selling = dbList[i+1];
                goalItem = boothTyped.selling;
            }
            else 
            {   //repeat booths, not officially part of sequence
                boothTyped.buying = dbList[Random.Range(0,dbList.Count)];
                boothTyped.selling = boothTyped.buying;
                while(boothTyped.selling == boothTyped.buying)
                {
                    boothTyped.selling = db.itemList[Random.Range(0, db.itemList.Count)];
                }
            }

            booth.transform.parent = this.transform.Find("Booths");

            booth.GetComponentInChildren<ChatCollision>().camController = controller;
            booth.GetComponentInChildren<ChatCollision>().player = player;
            kioskTransforms.Add(booth.transform);




            Vector3 boothPos = new Vector3();
            bool positionFound = false;
            while (!positionFound)
            {

                positionFound = true;

                boothPos.x = Random.Range(-levelSize + 5, levelSize - 5);
                boothPos.y = Random.Range(-levelSize + 5, levelSize - 5);
                foreach (Transform t in kioskTransforms)
                {
                    if (Vector3.Distance(boothPos, t.position) < minDistanceFromKiosk)
                    {
                        positionFound = false;
                    }
                }
            }
            booth.transform.position = boothPos;


            int spriteIndex = Random.Range(0, boothSprites.Count);
            booth.GetComponent<SpriteRenderer>().sprite = boothSprites[spriteIndex];
            boothSprites.RemoveAt(spriteIndex);
        }

        player.SetGoalItem(goalItem);


        Transform screnSpaceCanvas = Camera.main.transform.Find("Canvas");

        Transform previous = screnSpaceCanvas.Find("Goal Item");
        if(previous != null)
        {
            Destroy(previous.gameObject);
        }


        GameObject textGO = new GameObject("Goal Text");
        textGO.transform.position = new Vector3(0, 0, 0);
        Text goalText = textGO.AddComponent<Text>();
        goalText.rectTransform.SetParent(screnSpaceCanvas);
        goalText.transform.localPosition = new Vector3(0, 0, 0);
        goalText.rectTransform.anchorMin = new Vector4(0, 1);
        goalText.rectTransform.anchorMax = new Vector4(0, 1);
        goalText.rectTransform.anchoredPosition = new Vector3(100, -120, 0);
        
        goalText.rectTransform.localScale = new Vector3(1, 1, 1);
        goalText.fontSize = 32;
        goalText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        goalText.text = "You Want: ";

        GameObject goalGO = new GameObject("Goal Item");
        goalGO.transform.position = new Vector3(0, 0, 0);
        Image goalImg = goalGO.AddComponent<Image>();
        goalImg.rectTransform.SetParent(screnSpaceCanvas);
        goalImg.transform.localPosition = new Vector3(0, 0, 0);
        goalImg.rectTransform.anchorMin = new Vector2(0f, 1);
        goalImg.rectTransform.anchorMax = new Vector2(0f, 1);
        goalImg.rectTransform.anchoredPosition = new Vector3(200, -120, 0);
        
        goalImg.rectTransform.localScale = new Vector3(1f, 1f, 1f);
        goalImg.sprite = goalItem.icon;
    }

    private void GenerateWalls()
    {
        Transform wallParent = this.transform.Find("Walls"); 


        GameObject wallEast = Instantiate(wallPrefab);
        wallEast.transform.position = new Vector3(levelSize, 0, 0);
        wallEast.transform.localScale = new Vector3(1, levelSize * 6.25f, 1);
        wallEast.transform.parent = wallParent;

        GameObject wallWest = Instantiate(wallPrefab);
        wallWest.transform.position = new Vector3(-levelSize, 0, 0);
        wallWest.transform.localScale = new Vector3(1, levelSize * 6.25f, 1);
        wallWest.transform.parent = wallParent;

        GameObject wallNorth = Instantiate(wallPrefab);
        wallNorth.transform.position = new Vector3(0, levelSize, 0);
        wallNorth.transform.localScale = new Vector3(levelSize * 6.25f, 1, 1);
        wallNorth.transform.parent = wallParent;

        GameObject wallSouth = Instantiate(wallPrefab);
        wallSouth.transform.position = new Vector3(0, -levelSize, 0);
        wallSouth.transform.localScale = new Vector3(levelSize * 6.25f, 1, 1);
        wallSouth.transform.parent = wallParent;
    }
}
