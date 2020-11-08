using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Item currItem;
    private SpriteRenderer spriteRenderer;


    public void setupInventory(Item item)
    {
        currItem = item;
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