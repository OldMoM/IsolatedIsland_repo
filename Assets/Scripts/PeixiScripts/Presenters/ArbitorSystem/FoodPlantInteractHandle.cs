using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace Peixi
{
    public class FoodPlantInteractHandle
    {
        public IObservable<Unit> OnInteractStart => onInteractStart;
        public IObservable<Unit> OnInteractEnd => onInteractEnd;

        private Subject<Unit> onInteractStart = new Subject<Unit>();
        private Subject<Unit> onInteractEnd = new Subject<Unit>();
        private IInventorySystem InventorySystem => InterfaceArichives.Archive.IInventorySystem;

        private bool hasEoughMat;
        public void EndInteract()
        {
            InventorySystem.RemoveItem("freshFish", 1);
            InventorySystem.AddItem("grilledFish",1);
            onInteractEnd.OnNext(Unit.Default);
        }
        public void StartInteract()
        {
            var inventory = InterfaceArichives.Archive.IInventorySystem;
            var freshFishAmount = inventory.GetAmount("freshFish");

            hasEoughMat = freshFishAmount > 0;
            if (hasEoughMat)
            {
                onInteractStart.OnNext(Unit.Default);
            }
            else
            {
                var chatBubble = InterfaceArichives.Archive.InGameUIComponentsManager.ChatBubble;

                var msg = new string[1];
                msg[0] = "我没有足够材料";
                var player = InterfaceArichives.Archive.PlayerSystem.OnPlayerPositionChanged;
                chatBubble.StartChat(msg, player);

                onInteractEnd.OnNext(Unit.Default);
            }
        }
    }
}
