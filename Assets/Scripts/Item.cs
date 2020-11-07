using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int id;
    public string title;
    public Sprite icon;

    public Item(int id, string title, Sprite icon) 
    {
        this.id = id;
        this.title = title;
        this.icon = icon;
    }
}