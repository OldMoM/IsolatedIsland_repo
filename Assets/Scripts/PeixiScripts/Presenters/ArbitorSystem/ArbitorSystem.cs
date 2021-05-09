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
        private CollectInteractionHandle collectModule;
        private FacilityInteractionAgent _facilityInteractHandle;
        private InventorySystem inventorySystem;
        private ArbitorEyeModule eye;
        private IslandInteractAgent islandInteractAgent;
        private SuppleUseAgent suppleUseAgent;

        [SerializeField]
        private FacilityData facility;
        public IInventorySystem InventorySystem => inventorySystem;
        public CollectInteractionHandle CollectModule => collectModule;
        public ArbitorEyeModule ArbitorEye => eye;
        public IObservable<Unit> OnCollectCompleted => collectModule.OnCollectCompleted;
        public IObservable<Unit> OnAllCollectCompleted => ArbitorEye.AllCollectCompleted;
        public IObservable<BaseItem> HandleItem => ArbitorEye.HandleItem;
        public IObservable<int> OnCollectHandleCountChanged => eye.OnQuenedCountChanged;
        public IslandInteractAgent IslandInteractAgent => islandInteractAgent;
     
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

        public FacilityInteractionAgent facilityInteractAgent 
        {
            get
            {
                if (_facilityInteractHandle is null)
                {
                    _facilityInteractHandle = new FacilityInteractionAgent();
                }
                return _facilityInteractHandle;
            }
        }

        [SerializeField]
        [Tooltip("Collect互动过程的监视参数")]
        internal CollectWatchParam collectParams = new CollectWatchParam();

        private IslandInteractAgent CreateIslandInteractAgent()
        {
            var iPlayerSystem = InterfaceArichives.Archive.PlayerSystem;
            return new IslandInteractAgent(iPlayerSystem.OnPlayerPositionChanged);
        }

        // Start is called before the first frame update
        void Awake()
        {
            Singlton = this;
            inventorySystem = FindObjectOfType<InventorySystem>();
      
        }
        private void OnEnable()
        {
            collectModule = new CollectInteractionHandle(this);
            eye = new ArbitorEyeModule(collectModule);
        }
        void Start()
        {
            suppleUseAgent = new SuppleUseAgent(InterfaceArichives.Archive.InGameUIComponentsManager.InventoryGui.OnItemUsed);

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
        private void BuildFacilityAgentInit()
        {
            
        }
        private void Update()
        {
            facility = facilityInteractAgent.targetData;
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
