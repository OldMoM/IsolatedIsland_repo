using Caoye;
using IngameDebugConsole;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugDialogue : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DebugLogConsole.AddCommand<string>("startDialog", "进行对话", (stringId) =>
        {
            IDialogSystem testDialog = FindObjectOfType<DialogueSystem>();
            testDialog.StartDialog(stringId);
        });

        //DebugLogConsole.AddCommand<string, int>("removeItem", "", (name, amount) =>
        //{
        //    IInventorySystem iinventroy = FindObjectOfType<InventorySystem>();
        //    iinventroy.RemoveItem(name, amount);
        //});

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
