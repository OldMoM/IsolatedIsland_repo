using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

namespace Peixi
{
    /// <summary>
    /// 处理收集互动过程
    /// </summary>
    public class CollectInteractionHandle
    {
        private IArbitorSystem m_arbitor;
        private bool isProcessing;
        private Subject<Unit> onCollectCompleted = new Subject<Unit>();
        public IObservable<Unit> OnCollectCompleted => onCollectCompleted;

        public CollectInteractionHandle(IArbitorSystem arbitor)
        {
            m_arbitor = arbitor;
        }
        public CollectInteractionHandle StartCollect(BaseItem item)
        {
            var load = m_arbitor.InventorySystem.Load;
            var capacity = m_arbitor.InventorySystem.Capacity;
            var notFull = load < capacity;
            var hasTheItem = m_arbitor.InventorySystem.HasItem(item.data.name);

            if (notFull)
            {
                m_arbitor.InventorySystem.AddItem(item.data.name, item.data.amount);
                item.Recycle();
                onCollectCompleted.OnNext(Unit.Default);
                return this;
            }

            if (!notFull)
            {
                if (hasTheItem)
                {
                    m_arbitor.InventorySystem.AddItem(item.data.name, item.data.amount);
                    item.Recycle();
                    onCollectCompleted.OnNext(Unit.Default);
                    return this;
                }
            }

            onCollectCompleted.OnNext(Unit.Default);
            return this;
        }
    }
}
