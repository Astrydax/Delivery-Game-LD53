using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
   public enum ItemType
    {
        Dubloon,
        Package,
        Blockage
    }

    public ItemType itemType;
    public int amount;
}
