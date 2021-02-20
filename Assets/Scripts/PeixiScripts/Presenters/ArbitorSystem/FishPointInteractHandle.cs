using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

namespace Peixi
{
    public class FishPointInteractHandle 
    {
        public IObservable<Unit> onInteractStart => _onInteractStart;
        public IObservable<FishingResult> onInteractEnd => _onInteractEnd;

        private Subject<Unit> _onInteractStart = new Subject<Unit>();
        private Subject<FishingResult> _onInteractEnd = new Subject<FishingResult>();

        private IInventorySystem Inventory => InterfaceArichives.Archive.IInventorySystem;
        public void endInteract(FishingResult result)
        {
            _onInteractEnd.OnNext(result);
            Inventory.AddItem(ItemTags.String);
            Debug.Log(result.plastic);
        }
        public void startInteract()
        {
            _onInteractStart.OnNext(Unit.Default);
        }
    }
    public struct FishingResult
    {
        public int wood;
        public int plastic;
    }
}
