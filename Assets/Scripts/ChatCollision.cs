using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.UI;

public class ChatCollision : MonoBehaviour
{
    
    public CameraController camController;
    private Booth booth;
    private GameObject display;
    public Sprite bubbleSprite;

    public PlayerController player;


    private Image bubbleImage;
    private Image buyingImage;
    private Image sellingImage;
    private Text titleText;
    private Text forText;

    public void Start()
    {
        booth = gameObject.GetComponentInParent<Booth>();

        display = new GameObject("Booth Text");
        display.transform.parent = GameObject.Find("UI").transform.GetChild(1);
        display.transform.localScale = new Vector3(1, 1, 1);

        GameObject bubble = new GameObject("Bubble");
        bubbleImage = bubble.AddComponent<Image>();
        bubbleImage.rectTransform.SetParent(display.transform);
        bubbleImage.rectTransform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        bubbleImage.rectTransform.localPosition += new Vector3(35, 50, -1);
        bubbleImage.overrideSprite = bubbleSprite;
        bubbleImage.enabled = false;
        
        GameObject textGO = new GameObject("Title Text");
        titleText = textGO.AddComponent<Text>();
        titleText.rectTransform.SetParent(display.transform);
        titleText.rectTransform.localPosition += new Vector3(42, 53, 0);
        titleText.rectTransform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
        titleText.text = "I will trade";
        titleText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        titleText.color = Color.black;
        titleText.enabled = false;

        GameObject buyingGO = new GameObject("Buying");
        buyingImage = buyingGO.AddComponent<Image>();
        buyingImage.rectTransform.SetParent(display.transform);
        buyingImage.rectTransform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        buyingImage.rectTransform.localPosition += new Vector3(18, 55, 0);
        buyingImage.overrideSprite = booth.buying.icon;
        buyingImage.enabled = false;

        GameObject forTextGO = new GameObject("Space Text");
        forText = forTextGO.AddComponent<Text>();
        forText.rectTransform.SetParent(display.transform);
        forText.rectTransform.localPosition += new Vector3(47, 40, 0);
        forText.rectTransform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
        forText.text = "for";
        forText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        forText.color = Color.black;
        forText.enabled = false;

        GameObject sellingGO = new GameObject("Selling");
        sellingImage = sellingGO.AddComponent<Image>();
        sellingImage.rectTransform.SetParent(display.transform);
        sellingImage.rectTransform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        sellingImage.rectTransform.localPosition += new Vector3(50, 55, 0);
        sellingImage.overrideSprite = booth.selling.icon;
        sellingImage.enabled = false;

        display.transform.position = this.transform.position;
    }

    void Update()
    {
        if(booth.hasPurchased)
        {
            titleText.text = "Thank you for your purchase :)";
            sellingImage.enabled = false;
            buyingImage.enabled = false;
            forText.enabled = false;
        }
        else
        {
            titleText.text = "I will trade";
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.SetBooth(booth);

            camController.targetOrthoSize = 6;
            camController.targetPosition = this.transform;
            camController.offset = new Vector3(0, 2, 0);

            bubbleImage.enabled = true;
            titleText.enabled = true;
            if(!booth.hasPurchased)
            {
                buyingImage.enabled = true;
                sellingImage.enabled = true;
                forText.enabled = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            camController.targetOrthoSize = 12;
            camController.targetPosition = collision.gameObject.transform;
            camController.offset = new Vector3(0,0,0);

            player.SetBooth(null);

            bubbleImage.enabled = false;
            titleText.enabled = false;
            buyingImage.enabled = false;
            sellingImage.enabled = false;
            forText.enabled = false;
        }
    }
}
