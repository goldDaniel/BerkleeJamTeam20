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
            new Item(0, "Bear", spriteList[0]),
            new Item(1, "Car", spriteList[1]),
            new Item(2, "Book", spriteList[2]),
            new Item(3, "Duck", spriteList[3]),
            new Item(4, "Globe", spriteList[4]),
            new Item(5, "Carrot", spriteList[5]),
            new Item(6, "Spyglass", spriteList[6]),
            new Item(7, "Ukulele", spriteList[7]),
            new Item(8, "Paint", spriteList[8]),
            new Item(9, "Table", spriteList[9]),
            new Item(10, "Camera", spriteList[10]),
            new Item(11, "Map", spriteList[11]),
        };
    }
}