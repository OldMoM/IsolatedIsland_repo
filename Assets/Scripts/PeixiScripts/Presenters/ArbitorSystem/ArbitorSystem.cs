using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;


namespace Peixi
{
    /// <summary>
    /// 使用适配器模式，将各种交互功能模块封装在这个脚本
    /// </summary>
    public class ArbitorSystem : MonoBehaviour,IArbitorSystem
    {
        private CollectHandle collectModule;
        private InventorySystem inventorySystem;
        private ArbitorEyeModule eye;
        public IInventorySystem InventorySystem => inventorySystem;
        public CollectHandle CollectModule => collectModule;
        public ArbitorEyeModule ArbitorEye => eye;
        public IObservable<Unit> OnCollectCompleted => collectModule.OnCollectCompleted;
        public IObservable<Unit> OnAllCollectCompleted => ArbitorEye.AllCollectCompleted;
        public IObservable<BaseItem> HandleItem => ArbitorEye.HandleItem;
        public IObservable<int> OnCollectHandleCountChanged => eye.OnQuenedCountChanged;
        private static IArbitorSystem arbitor;
        public static IArbitorSystem Singlton
        {
            get => arbitor;
            set
            {
                if (arbitor == null)
                {
                    arbitor = value;
                }
            }
        }

        [SerializeField]
        [Tooltip("Collect互动过程的监视参数")]
        internal CollectWatchParam collectParams = new CollectWatchParam();

        // Start is called before the first frame update
        void Awake()
        {
            Singlton = this;
            inventorySystem = FindObjectOfType<InventorySystem>();
      
        }
        private void OnEnable()
        {
            collectModule = new CollectHandle(this);
            eye = new ArbitorEyeModule(collectModule);
        }
        void Start()
        {
            eye.OnQuenedCountChanged
                .Subscribe(x =>
                {
                    collectParams.queneCount = x;
                });

            eye.HandleItem
                .Subscribe(x =>
                {
                    if (x != null)
                    {
                        collectParams.handleItemName = x.data.name;
                        collectParams.handleItemHasCode = x.GetHashCode();
                    }
                    else
                    {
                        collectParams.handleItemName = "None";
                        collectParams.handleItemHasCode = 0;
                    }
                });

            OnCollectHandleCountChanged
                .Where(x => x == 0)
                .Subscribe(y =>
                {
                    collectParams.handleItemName = "None";
                    collectParams.handleItemHasCode = 0;
                });
        }
        public void OnPlayerTouch(BaseItem item)
        {
            eye.OnPlayerTouch(item);
        }
        public void OnPlayerUntouch(BaseItem item)
        {
            eye.OnPlayerUnTouch(item);
        }
        public void OnItemRecycle(BaseItem item) 
        {
            eye.RemoveItemFromPendingQuene(item);
        }
    }

    [Serializable]
    public struct CollectWatchParam
    {
        [Tooltip("Collect业务队列长度")]
        public int queneCount;
        [Tooltip("当前办理Collect业务Item名字")]
        public string handleItemName;
        [Tooltip("当前办理Collect业务Item的HashCode")]
        public int handleItemHasCode;
    }
}
