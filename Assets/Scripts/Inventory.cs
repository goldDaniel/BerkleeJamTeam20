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
        renderItem();
    }

    private void renderItem()
    {
        spriteRenderer.sprite = currItem.icon;
    }

    public int tradeItem(int buyIndex)
    {
        //int sellIndex = currItemIndex;
        //currItemIndex = buyIndex;
        //renderItem();
        //return sellIndex;
        return 0;
    }
}