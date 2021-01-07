using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IngameDebugConsole;
using Peixi;

public class InventorySystemIntegrationTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DebugLogConsole.AddCommand<string, int>("addItem", "向背包中添加物品", (name, amount) =>
        {
            IInventorySystem iinventroy = FindObjectOfType<InventorySystem>();
            iinventroy.AddItem(name, amount);
        });

        DebugLogConsole.AddCommand<string, int>("removeItem", "", (name, amount) =>
        {
            IInventorySystem iinventroy = FindObjectOfType<InventorySystem>();
            iinventroy.RemoveItem(name, amount);
        });

    }
}
