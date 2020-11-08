using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject boothPrefab;
    public GameObject wallPrefab;

    public CameraController controller;
    public PlayerController player;

    public int level = 1;

    public List<Sprite> boothSprites;

    private float levelSize;

    public float levelTime;

    // Start is called before the first frame update
    void Start()
    {
        float level1Size = 25;
        levelSize = level1Size + level1Size * (level - 1) * 1.1f;

        levelTime = 100; //update later with formula
        
        GenerateKiosks();
        GenerateWalls();
    }

    private void GenerateKiosks()
    {
        List<Transform> kioskTransforms = new List<Transform>();
        float minDistanceFromKiosk = 15;
        int boothCount = 4 + (2 * (level - 1));

        ItemDatabase db = GameObject.FindGameObjectWithTag("Item Database").GetComponent<ItemDatabase>();
        List<Item> dbList = new List<Item>(db.itemList);
        Item playerItem = player.inventory.currItem;
        dbList.Remove(playerItem);
        //shuffle dbList
        for (int i = dbList.Count - 1; i >= 1; i--){
            int randomIndex = Random.Range(0,i+1);
            Item temp = dbList[randomIndex];
            dbList[randomIndex] = dbList[i];
            dbList[i] = temp;
        }
        dbList.Insert(0,playerItem);

        for (int i = 0; i < boothCount; i++)
        {
            GameObject booth = Instantiate(boothPrefab);

            Booth boothTyped = booth.GetComponentInParent<Booth>();

            if (i+1 < dbList.Count){
                boothTyped.buying = dbList[i];
                boothTyped.selling = dbList[i+1];
            }
            else { //repeat booths, not officially part of sequence
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


            Vector3 pos = new Vector3();


            bool positionFound = false;
            while (!positionFound)
            {
                positionFound = true;

                pos.x = Random.Range(-levelSize + 2, levelSize - 2);
                pos.y = Random.Range(-levelSize + 2, levelSize - 2);
                foreach (Transform t in kioskTransforms)
                {
                    if (Vector3.Distance(pos, t.position) < minDistanceFromKiosk)
                    {
                        positionFound = false;
                    }
                }
            }
            booth.transform.position = pos;


            int spriteIndex = Random.Range(0, boothSprites.Count);
            booth.GetComponent<SpriteRenderer>().sprite = boothSprites[spriteIndex];
            boothSprites.RemoveAt(spriteIndex);
        }
    }

    private void GenerateWalls()
    {
        Transform wallParent = this.transform.Find("Walls"); 
        

        

        GameObject wallEast = Instantiate(wallPrefab);
        wallEast.transform.position = new Vector3(levelSize, 0, 0);
        wallEast.transform.localScale = new Vector3(1, levelSize * 2, 1);
        wallEast.transform.parent = wallParent;

        GameObject wallWest = Instantiate(wallPrefab);
        wallWest.transform.position = new Vector3(-levelSize, 0, 0);
        wallWest.transform.localScale = new Vector3(1, levelSize * 2, 1);
        wallWest.transform.parent = wallParent;

        GameObject wallNorth = Instantiate(wallPrefab);
        wallNorth.transform.position = new Vector3(0, levelSize, 0);
        wallNorth.transform.localScale = new Vector3(levelSize * 2, 1, 1);
        wallNorth.transform.parent = wallParent;

        GameObject wallSouth = Instantiate(wallPrefab);
        wallSouth.transform.position = new Vector3(0, -levelSize, 0);
        wallSouth.transform.localScale = new Vector3(levelSize * 2, 1, 1);
        wallSouth.transform.parent = wallParent;
    }
}
