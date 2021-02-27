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
        private Transform gridManagers_tran;
        private Transform descriptions_tran;

        private Text descriptionLabel;
        private Button useBtn;
        private Button holdBtn;
        private Button throwBtn;

        List<GameObject> childrendGameObjects = new List<GameObject>();

        InventoryDescriptionWidge descriptionWidge;

        IInventorySystem iinventroy;

        bool isOpened = false;

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
              isOpened = !isOpened;

              gridManagers_tran.gameObject.SetActive(isOpened);
              back_tran.gameObject.SetActive(isOpened);
              useBtn.gameObject.SetActive(isOpened);
              holdBtn.gameObject.SetActive(isOpened);
              throwBtn.gameObject.SetActive(isOpened);
              descriptions_tran.gameObject.SetActive(isOpened);
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
                var grid_gameobject = GameObject.Instantiate(gridPrefab, gridManagers_tran);
                grid_gameobject.transform.name = "Grid" + i;
                grids.Add(grid_gameobject);

                var gridScript = grid_gameobject.GetComponent<GridHandleView>();
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
            iinventroy = InterfaceArichives.Archive.IInventorySystem;
            gridManagers_tran = transform.Find("GridsManager");
            back_tran = transform.Find("Background");
            descriptionLabel = transform
                .Find("DescriptionWidge")
                .Find("DescriptionLabel")
                .GetComponent<Text>();
            useBtn = transform.Find("UseBtn").GetComponent<Button>();
            holdBtn = transform.Find("HoldBtn").GetComponent<Button>();
            throwBtn = transform.Find("ThrowBtn").GetComponent<Button>();
            descriptions_tran = transform.Find("DescriptionWidge");

            childrendGameObjects
                .AddItem(gridManagers_tran.gameObject)
                .AddItem(back_tran.gameObject)
                .AddItem(descriptions_tran.gameObject)
                .AddItem(useBtn.gameObject)
                .AddItem(holdBtn.gameObject)
                .AddItem(throwBtn.gameObject);

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
               .React(OnUseBtnClicked);

        }
    }
}
