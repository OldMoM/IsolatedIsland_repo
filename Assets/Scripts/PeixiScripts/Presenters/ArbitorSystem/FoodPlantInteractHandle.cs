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
        public void EndInteract()
        {
            InventorySystem.AddItem(ItemTags.bread,1);
            onInteractEnd.OnNext(Unit.Default);
        }
        public void StartInteract()
        {
            onInteractStart.OnNext(Unit.Default);
        }
    }
}
