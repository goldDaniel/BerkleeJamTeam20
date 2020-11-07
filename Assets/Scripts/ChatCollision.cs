using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ChatCollision : MonoBehaviour
{
    
    public CameraController camController;
    
    

    public Sprite[] possibleSprites;

    private int sellIndex;
    private int buyIndex;

    private GameObject display;


    public void Start()
    {
        sellIndex = Random.Range(0, possibleSprites.Length);

        buyIndex = sellIndex;
        while(buyIndex == sellIndex)
        {
            buyIndex = Random.Range(0, possibleSprites.Length);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            camController.targetOrthoSize = 6;
            camController.targetPosition = this.transform;
        }

        display = new GameObject("Booth Text");
        display.transform.parent = GameObject.Find("UI").transform.GetChild(1);
        display.transform.localScale = new Vector3(1, 1, 1);


        GameObject textGO = new GameObject("Title Text");
        Text text = textGO.AddComponent<Text>();
        text.rectTransform.SetParent(display.transform);
        text.rectTransform.localPosition += new Vector3(6, 10, 0);
        text.rectTransform.localScale = new Vector3(0.55f, 0.55f, 0.55f);
        text.text = "I will trade";
        text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");


        GameObject buyingGO = new GameObject("Buying");
        Image image = buyingGO.AddComponent<Image>();
        image.rectTransform.SetParent(display.transform);
        image.rectTransform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        image.rectTransform.localPosition += new Vector3(-16, 0, 0);
        image.overrideSprite = possibleSprites[buyIndex];


        GameObject forTextGO = new GameObject("Space Text");
        text = forTextGO.AddComponent<Text>();
        text.rectTransform.SetParent(display.transform);
        text.rectTransform.localPosition += new Vector3(24, -16, 0);
        text.rectTransform.localScale = new Vector3(0.55f, 0.55f, 0.55f);
        text.text = "for";
        text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");

        GameObject sellingGO = new GameObject("Selling");
        image = sellingGO.AddComponent<Image>();
        image.rectTransform.SetParent(display.transform);
        image.rectTransform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        image.rectTransform.localPosition += new Vector3(16, 0, 0);
        image.overrideSprite = possibleSprites[sellIndex];


        display.transform.position = this.transform.position;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            camController.targetOrthoSize = 12;
            camController.targetPosition = collision.gameObject.transform;
        }

        Destroy(display);
        display = null;
    }
}
