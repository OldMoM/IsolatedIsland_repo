using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UniRx;
using Siwei;

namespace Peixi
{
    public class InGameUIComponents : MonoBehaviour,IInGameUIComponents
    {
        public PlayerPropertyHUD PlayerPropertyHUD
        {
            get
            {
                if (playerPropertyHUD is null)
                {
                    playerPropertyHUD = transform.Find(UIComponentsTags.PlayerPropertyHUD).GetComponent<PlayerPropertyHUD>();
                }
                return playerPropertyHUD;
            }
        }
        public IChatBubble ChatBubble
        {
            get
            {
                if (chatBubble is null)
                {
                    var chatBubble_tran = transform.Find("DialogueBox");
                    Assert.IsNotNull(chatBubble_tran, "Failed to find DialogueBox.prefab");
                    chatBubble = chatBubble_tran.GetComponent<ChatBubble>();
                    Assert.IsNotNull(chatBubble, "Failed to find IChatBubble at DialogueBox.prefab");
                }
                return chatBubble;    
            }
        }
        public IInventoryGui InventoryGui
        {
            get
            {
                if (inventoryGui is null)
                {
                    inventoryGui = transform.Find(UIComponentsTags.InventoryGuiWindow).GetComponent<InventoryGui>();
                }
                return inventoryGui;
            }
        }

        private IInventoryGui inventoryGui;
        private PlayerPropertyHUD playerPropertyHUD;
        private IChatBubble chatBubble;


    }
}
