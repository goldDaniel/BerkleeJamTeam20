using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> itemList;
    public Sprite[] spriteList;

    private void Awake()
    {
        itemList = new List<Item>() {
            new Item(0, "Blue", spriteList[0]),
            new Item(1, "Green", spriteList[1]),
            new Item(2, "Gray", spriteList[2]),
            new Item(3, "Purple", spriteList[3]),
            new Item(4, "Red", spriteList[4]),
            new Item(5, "Yellow", spriteList[5])
        };
    }
}