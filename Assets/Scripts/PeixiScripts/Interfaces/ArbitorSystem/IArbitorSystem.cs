using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

namespace Peixi
{
    public interface IArbitorSystem
    {
        IInventorySystem InventorySystem { get; }
        CollectInteractionHandle CollectModule { get; }
        ArbitorEyeModule ArbitorEye { get; }
        void OnPlayerTouch(BaseItem item);
        void OnPlayerUntouch(BaseItem item);
        IObservable<Unit> OnCollectCompleted { get; }
        IObservable<Unit> OnAllCollectCompleted { get; }
        /// <summary>
        /// 当前正在进行Collect互动的Item
        /// </summary>
        IObservable<BaseItem> HandleItem { get; }
        IObservable<int> OnCollectHandleCountChanged { get; }
        void OnItemRecycle(BaseItem itemHashCode);

        FacilityInteractionAgent facilityInteractionHandle { get; }
    }
}
