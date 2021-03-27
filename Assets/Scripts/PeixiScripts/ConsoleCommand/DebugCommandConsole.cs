using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IngameDebugConsole;


namespace Peixi
{
    public class DebugCommandConsole : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            DebugLogConsole.AddCommand<string,int>("inventory.additem", "",InventoryCommands.AddItem);
        }


    }
}
