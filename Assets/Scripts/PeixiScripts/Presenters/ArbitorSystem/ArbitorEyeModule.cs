using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using System.Linq;
using System.Runtime.CompilerServices;
[assembly:InternalsVisibleTo("EditModeTests")]

namespace Peixi
{
    public class ArbitorEyeModule
    {
        CollectHandle collectHandle;
        private PlayerSystem playerSystem;

        Subject<int> onItemQueneCountChanged = new Subject<int>();
        Subject<BaseItem> onPlayerTouchItem = new Subject<BaseItem>();
        Subject<BaseItem> onPlayerUntouchItem = new Subject<BaseItem>();
        Subject<Unit> onAllCollectCompleted = new Subject<Unit>();

        ReactiveProperty<BaseItem> currentHandleItem = new ReactiveProperty<BaseItem>();

        BaseItem nextHandleItem;

        [Obsolete]
        protected List<BaseItem> quenedItems = new List<BaseItem>();

        protected ReactiveDictionary<int, BaseItem> pendingItems = new ReactiveDictionary<int, BaseItem>();
        public int PendingItemCount => pendingItems.Count;
        public IObservable<int> OnQuenedCountChanged => pendingItems.ObserveCountChanged();
        public IObservable<BaseItem> OnPlayerTouchItem => onPlayerTouchItem;
        public IObservable<BaseItem> OnPlayerUntouchItem => onPlayerUntouchItem;
        public IObservable<Unit> AllCollectCompleted => onAllCollectCompleted;
        public IObservable<BaseItem> HandleItem => currentHandleItem;
    
        IDisposable releasdInteractBtnPressedThread;
        public ArbitorEyeModule(CollectHandle handle)
        {
            collectHandle = handle;
            playerSystem = GameObject.FindObjectOfType<PlayerSystem>();

            collectHandle.OnCollectCompleted
                .Where(x => pendingItems.Count > 0)
                .Subscribe(y =>
                {

                });

            InputSystem.Singleton.OnInteractBtnReleased
                .Where(x => pendingItems.Count > 0)
                .Subscribe(x =>
                {
                    ShowMessage.singlton.Message("");
                    if (currentHandleItem.Value != null)
                    {
                        var hashcode1 = currentHandleItem.Value.GetHashCode();
                        collectHandle.StartCollect(currentHandleItem.Value);

                        currentHandleItem.Value = null;
                        pendingItems.Remove(hashcode1);
                        if (pendingItems.Count > 0)
                        {
                            var hashcode2 = GetNearestItemHashcode(playerSystem.transform.position);
                            currentHandleItem.Value = pendingItems[hashcode2];
                        }
                    }  
                });

            pendingItems.ObserveAdd()
                .Subscribe(x =>
                {
                    var playerPos = playerSystem.transform.position;
                    var theHashCode = GetNearestItemHashcode(playerPos);
                    currentHandleItem.Value = pendingItems[theHashCode];
                });

            pendingItems.ObserveRemove()
                .Subscribe(x =>
                {
                    var playerPos = playerSystem.transform.position;
                    var theHashCode = GetNearestItemHashcode(playerPos);
                    if (pendingItems.Count > 0)
                    {
                        currentHandleItem.Value = pendingItems[theHashCode];
                    }
                    else
                    {
                        currentHandleItem.Value = null;
                    }
                });
        }
        public ArbitorEyeModule() { }
        public void OnPlayerTouch(BaseItem item)
        {
            ShowMessage.singlton.Message("按下互动键收集资源");
            AddItemToPendingQuene(item);
        }
        public void OnPlayerUnTouch(BaseItem item)
        {
            ShowMessage.singlton.Message("");
            RemoveItemFromPendingQuene(item);
        }
        internal bool HasItem(BaseItem item)
        {
            bool has = pendingItems.ContainsKey(item.GetHashCode());
            return has;
        }
        internal void AddItemToPendingQuene(BaseItem item)
        {
            var hashcode = item.GetHashCode();
            if (!pendingItems.ContainsKey(hashcode))
            {
                pendingItems.Add(hashcode, item);
            }
        }
        internal void RemoveItemFromPendingQuene(BaseItem item)
        {
            if (HasItem(item))
            {
                pendingItems.Remove(item.GetHashCode());
            }
        }
        protected int GetNearestItemHashcode(Vector3 playerPos)
        {
            float minDis = 999999;
            int hashCode = 0;
            pendingItems
                .Where(x=>x.Value != null)
                .Select(x =>
                {
                    var dis = Vector3.Distance(playerPos, x.Value.transform.position);
                    var _hashCode = x.Value.GetHashCode();
                    return new ValueTuple<float, int>(dis, _hashCode);
                })
                .Where(y => 
                {
                    bool lessThanMin = false;
                    lessThanMin = y.Item1 < minDis;
                    minDis = y.Item1;
                    return lessThanMin;
                })
                .ToObservable()
                .Subscribe(z =>
                {
                    hashCode = z.Item2;
                });
            return hashCode;
        }
    }
}
