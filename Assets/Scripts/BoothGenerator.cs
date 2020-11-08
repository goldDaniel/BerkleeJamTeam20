using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoothGenerator : MonoBehaviour
{

    public GameObject boothPrefab;
    public CameraController controller;
    public PlayerController player;

    public int level = 1;

    public List<Sprite> boothSprites;

    // Start is called before the first frame update
    void Start()
    {

        List<Transform> kioskTransforms = new List<Transform>();

        float minDistanceFromKiosk = 15;

        int boothCount = 4 + (2 * (level - 1));

        for(int i = 0; i < boothCount; i++)
        {
            GameObject booth = Instantiate(boothPrefab);
            booth.transform.parent = this.transform;

            booth.GetComponentInChildren<ChatCollision>().camController = controller;
            booth.GetComponentInChildren<ChatCollision>().player = player;
            kioskTransforms.Add(booth.transform);




            Vector3 pos = new Vector3();
            

            bool positionFound = false;
            while (!positionFound)
            {
                positionFound = true;

                pos.x = Random.Range(-20, 20);
                pos.y = Random.Range(-20, 20);
                foreach (Transform t in kioskTransforms)
                {
                    if(Vector3.Distance(pos, t.position) < minDistanceFromKiosk)
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
}
