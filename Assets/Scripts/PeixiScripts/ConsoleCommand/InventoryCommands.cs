using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCommands 
{
    public static void AddItem(string item,int amount)
    {
        var iinventorySystem = InterfaceArichives.Archive.IInventorySystem;
        iinventorySystem.AddItem(item,amount);
    }
}
