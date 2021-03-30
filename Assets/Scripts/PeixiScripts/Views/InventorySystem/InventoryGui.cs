using Extensions;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UniRx.Triggers;


namespace Peixi
{
    [HelpURL("https://spicegamestudio.pingcode.com/wiki/spaces/5fd0cd48fee2547e79b1b100/pages/5ff96925890884b318232467")]
    public class InventoryGui : MonoBehaviour, IInventoryGui
    {
        #region//Variables 
        public GameObject gridPrefab;
        List<GameObject> grids = new List<GameObject>();
        List<GridHandleView> gridScripts = new List<GridHandleView>();

        private Transform back_tran;
        [SerializeField]private Transform gridManagers_tran;
        private Transform descriptions_tran;

        private Text descriptionLabel;
        private Button useBtn;
        private Button holdBtn;
        private Button throwBtn;
        private Button closeBtn;

        List<GameObject> childrendGameObjects = new List<GameObject>();

        InventoryDescriptionWidge descriptionWidge;

        IInventorySystem iinventroy;

        private BoolReactiveProperty isOpened = new BoolReactiveProperty(false);
        private Subject<string> onItemUsed = new Subject<string>();


        [SerializeField]
        [Tooltip("当前选中的Item")]
        private StringReactiveProperty selectedItem = new StringReactiveProperty("None");
        [SerializeField]
        [Tooltip("指针点击的Item")]
        private StringReactiveProperty clickedItem = new StringReactiveProperty("None");
        #endregion

        #region//Publics
        public IObservable<string> OnSelectedItemChanged => selectedItem;
        public IObservable<string> OnClickedItemChanged => clickedItem;
        public string SelectedItem
        {
            get => selectedItem.Value;
            set
            {
                selectedItem.Value = value;
            }
        }
        public string ClickedItem => clickedItem.Value;
        public IObservable<bool> OnOpenStateChanged => isOpened;
        public IObservable<string> OnItemUsed => onItemUsed;
        public void Init(IInventorySystem inventorySystem)
        {
            Config()
                .Asserts()
                .SetActive(false)
                .InitGrids()
                .InitDescrptionWidge()
                .React(OnInventoryChanged)
                .React(OnItemClicked);
        }
        public void OnInventoryBtnPressed()
        {
            var isOpen = isOpened.Value;
            isOpened.Value = isOpen;
            Debug.Log(isOpened.Value);
              gridManagers_tran.gameObject.SetActive(isOpen);
              back_tran.gameObject.SetActive(isOpen);
              useBtn.gameObject.SetActive(isOpen);
              holdBtn.gameObject.SetActive(isOpen);
              throwBtn.gameObject.SetActive(isOpen);
              descriptions_tran.gameObject.SetActive(isOpen);
        }
        public void OnPointerEnterGrid(int gridSerial,string name)
        {
            selectedItem.Value = name;
        }
        public void OnPointerExitGrid(int gridSerial)
        {
            selectedItem.Value = "None";
        }
        public void OnPointerClickGrid(int gridSerial,string name)
        {
            clickedItem.Value = name;
        }
        public InventoryGui SetActive(bool active)
        {
            childrendGameObjects.ForEach(x =>
            {
                x.SetActive(active);
            });

            isOpened.Value = active;
            return this;
        }

        #endregion

        #region//Privates
        private void SetGridContent(int gridNum, string name, int amount)
        {
            var gridHandle = gridScripts[gridNum];
            gridHandle.itemName = name;
            gridHandle.itemAmount = amount;
        }
        private void ClearGridContent(int gridNum)
        {
            var gridHandle = gridScripts[gridNum];
            gridHandle.clearGrid();
        }
        private InventoryGui InitGrids()
        {
            
            for (int i = 0; i < iinventroy.Capacity; i++)
            {
                var gridEntity = Instantiate(gridPrefab, gridManagers_tran);
                gridEntity.transform.parent = gridManagers_tran;
                gridEntity.transform.name = "Grid" + i;
                grids.Add(gridEntity);

                var gridScript = gridEntity.GetComponent<GridHandleView>();
                gridScripts.Add(gridScript);
                gridScript.Active(this, i);
                ClearGridContent(i);
            }
            return this;
        }
        private InventoryGui InitDescrptionWidge()
        {
            descriptionWidge = transform
                .Find("DescriptionWidge")
                .GetComponent<InventoryDescriptionWidge>();
            Assert.IsNotNull(descriptionWidge, "descriptionWidge is null at " + name);
            descriptionWidge.init(this);

            return this;
        }
        private InventoryGui Asserts()
        {
            Assert.IsNotNull(descriptionLabel);
            Assert.IsNotNull(useBtn);
            Assert.IsNotNull(holdBtn);
            Assert.IsNotNull(throwBtn);
            return this;
        }
        private InventoryGui Config()
        {
            //获取外部依赖
            iinventroy = InterfaceArichives.Archive.IInventorySystem;
            //获取UI功能组件
            back_tran = transform.Find("Background");
            descriptionLabel = transform
                .Find("DescriptionWidge")
                .Find("DescriptionLabel")
                .GetComponent<Text>();
            useBtn = transform.Find("UseBtn").GetComponent<Button>();
            holdBtn = transform.Find("HoldBtn").GetComponent<Button>();
            throwBtn = transform.Find("ThrowBtn").GetComponent<Button>();
            closeBtn = transform.Find("CloseBtn").GetComponent<Button>();
            descriptions_tran = transform.Find("DescriptionWidge");
            //获取UI组件的Gameobject
            foreach (Transform child in transform)
            {
                childrendGameObjects.Add(child.gameObject);
            }
            //设置初始状态为false
            SetActive(false);
            return this;
        }
        private InventoryGui React(Action action)
        {
            action();
            return this;
        }
        private void OnInventoryChanged()
        {
            iinventroy.OnInventoryChanged
                .Subscribe(x =>
                {
                    var position = x.NewValue.Position;
                    var name = x.NewValue.Name;
                    var amount = x.NewValue.Amount;
                    var isEmpty = x.NewValue.IsEmpty;
                    if (!x.NewValue.IsEmpty)
                    {
                        SetGridContent(position, name, amount);
                    }
                    else
                    {
                        ClearGridContent(position);
                    }
                });
        }
        private void OnItemClicked()
        {
            clickedItem
               .Where(x=>x == "None" || x==string.Empty)
               .Subscribe(x =>
               {
                   descriptionLabel.text = string.Empty;
                   useBtn.interactable = false;
               });

            clickedItem
                .Where(x => x != "None")
                .Subscribe(x =>
                {
                    descriptionLabel.text = x;
                    useBtn.interactable = true;
                });

            
        }
        private void OnUseBtnClicked()
        {
            useBtn.OnPointerClickAsObservable()
                .Subscribe(x =>
                {
                    PotionUseAgent agent = new PotionUseAgent();
                    agent.UsePotion(ClickedItem);
                    onItemUsed.OnNext(ClickedItem);

                    AudioEvents.StartAudio("OnItemUsed");
                });
        }
        private void OnCloseBtnClicked()
        {
            closeBtn.OnPointerClickAsObservable()
                .Subscribe(x =>
                {
                    SetActive(false);
                });
        }
        #endregion

        private void Start()
        {
            Config()
               .Asserts()
               .SetActive(false)
               .InitGrids()
               .InitDescrptionWidge()
               .React(OnInventoryChanged)
               .React(OnItemClicked)
               .React(OnUseBtnClicked)
               .React(OnCloseBtnClicked);

        }
    }
}
