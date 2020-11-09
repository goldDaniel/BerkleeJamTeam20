
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public Vector3 velocity;
    public Rigidbody2D body;

    public Sprite bubbleSprite;

    private Animator animator;

    private AudioSource walkSound;
    private AudioSource tradeSound;
    private AudioSource denySound;


    private Booth booth;
    public Inventory inventory;

    Image bubbleImage;
    Image goalItemImage;
    Text bubbleText;

    Item goalItem;

    private float goalItemTime = 4;
    private float goalItemTimer = 4;

    // Start is called before the first frame update
    void Start()
    {
        booth = null;
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        walkSound = transform.Find("WalkSound").GetComponent<AudioSource>();
        tradeSound = transform.Find("TradeSound").GetComponent<AudioSource>();
        denySound = transform.Find("DenyTrade").GetComponent<AudioSource>();

        Transform parent = GameObject.Find("UI").transform.Find("Canvas");

        GameObject bubble = new GameObject("Bubble");
        bubbleImage = bubble.AddComponent<Image>();
        bubbleImage.rectTransform.SetParent(parent);
        bubbleImage.rectTransform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        bubbleImage.rectTransform.localPosition = this.transform.position;
        bubbleImage.overrideSprite = bubbleSprite;
        bubbleImage.enabled = false;

        GameObject textGO = new GameObject("Title Text");
        bubbleText = textGO.AddComponent<Text>();
        bubbleText.rectTransform.SetParent(parent);
        bubbleText.rectTransform.localPosition = this.transform.position;
        bubbleText.rectTransform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
        bubbleText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        bubbleText.color = Color.black;
        bubbleText.enabled = false;
    }

    public void SetGoalItem(Item item)
    {
        goalItemTimer = goalItemTime;

        this.goalItem = item;

        Transform parent = GameObject.Find("UI").transform.Find("Canvas");

        GameObject goalItemGO = new GameObject("Goal item");
        goalItemImage = goalItemGO.AddComponent<Image>();
        goalItemImage.rectTransform.SetParent(parent);
        goalItemImage.rectTransform.localScale = new Vector3(0.225f, 0.225f, 0.225f);
        goalItemImage.rectTransform.localPosition = this.transform.position;
        goalItemImage.overrideSprite = item.icon;
        goalItemImage.enabled = false;

    }

    void Update()
    {
        if( Input.GetKey(KeyCode.W) || 
            Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.D))
        {
            if(!walkSound.isPlaying)
            {
                walkSound.Play(0);
            }
        }

        if (Input.GetKey(KeyCode.T) && booth != null)
        {
            if (inventory.currItem == booth.buying)
            {
                if(!booth.hasPurchased)
                {
                    tradeSound.Play(0);
                }

                inventory.tradeItem(booth.selling);
                booth.hasPurchased = true;
            }
            else
            {
                if(!booth.hasPurchased)
                {
                    if (!denySound.isPlaying)
                    {
                        denySound.Play(0);
                    }
                }    
            }
        }

        goalItemTimer -= Time.deltaTime;
        if(goalItemTimer <= 0)
        {
            goalItemTimer = 0;
            bubbleImage.enabled = false;
            bubbleText.enabled = false;
            goalItemImage.enabled = false;
        }
        else
        {
            bubbleImage.enabled = true;
            bubbleText.enabled = true;
            goalItemImage.enabled = true;
            
            bubbleImage.rectTransform.position = this.transform.position;
            bubbleImage.rectTransform.position += new Vector3(1, 5, 0);
            
            bubbleText.rectTransform.position = this.transform.position;
            bubbleText.rectTransform.position += new Vector3(1, 5, 0);

            bubbleText.text = "I want to get";

            goalItemImage.rectTransform.position = this.transform.position;
            goalItemImage.rectTransform.position += new Vector3(1.5f, 5, 0);
        }
    }


    public void ShowLoseDialog()
    {
        bubbleImage.rectTransform.position = this.transform.position;
        bubbleImage.rectTransform.position += new Vector3(1, 5, 0);
        bubbleImage.enabled = true;


        bubbleText.rectTransform.position = this.transform.position;
        bubbleText.rectTransform.position += new Vector3(1, 5, 0);
        bubbleText.text = "Aww I ran out of time :(";
        bubbleText.enabled = true;
    }

    public void ShowWinDialog()
    {
        bubbleImage.rectTransform.position = this.transform.position;
        bubbleImage.rectTransform.position += new Vector3(1, 5, 0);
        bubbleImage.enabled = true;


        bubbleText.rectTransform.position = this.transform.position;
        bubbleText.rectTransform.position += new Vector3(1, 5, 0);
        bubbleText.text = "Yes I got what I wanted :)";
        bubbleText.enabled = true;
    }

    public void Reset()
    {
        gameObject.transform.position = new Vector3(0, 0, 0);
        bubbleText.enabled = false;
        bubbleImage.enabled = false;
    }

    public void SetBooth(Booth booth)
    {
        this.booth = booth;
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

        animator.SetFloat("YMovement", velocity.y); 
    }
}
