using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Item currItem;

    public ItemDatabase itemDatabase;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        currItem = itemDatabase.itemList[Random.Range(0, itemDatabase.itemList.Count)];
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = currItem.icon;
    }


    public Item tradeItem(Item buyItem)
    {
        Item sellItem = currItem;
        currItem = buyItem;
        spriteRenderer.sprite = currItem.icon;
        return sellItem;
    }
}