using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

namespace Peixi
{
    public class FishPointInteractAgent 
    {
        public IObservable<Unit> onInteractStart => _onInteractStart;
        public IObservable<FishingResult> onInteractEnd => _onInteractEnd;

        private Subject<Unit> _onInteractStart = new Subject<Unit>();
        private Subject<FishingResult> _onInteractEnd = new Subject<FishingResult>();

        private IInventorySystem Inventory => InterfaceArichives.Archive.IInventorySystem;
        public void endInteract(bool result)
        {
            if (result)
            {
                Inventory.AddItem("fiber", 2);
                Inventory.AddItem("plastic", 2);
            }
            var fishResult = new FishingResult();
            _onInteractEnd.OnNext(fishResult);
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
