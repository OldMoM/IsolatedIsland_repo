using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;
using UnityEngine.Assertions;


namespace Peixi
{
    [HelpURL("https://spicegamestudio.pingcode.com/wiki/spaces/5fd0cd48fee2547e79b1b100/pages/5ff96925890884b318232467")]
    public class InventoryGui : MonoBehaviour, IInventoryGui
    {
        #region//Variables 
        public GameObject gridPrefab;
        List<GameObject> grids = new List<GameObject>();
        List<GridHandleView> gridScripts = new List<GridHandleView>();

        Transform back;
        Transform gridManagers;
        Transform descriptions;
        Text descriptionLabel;
        Button useBtn;
        Button holdBtn;
        Button throwBtn;
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
        public void init(IInventorySystem inventorySystem)
        {
            getReference(inventorySystem);

            asserts();

            initGrids();

            initDescrptionWidge();

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

            clickedItem
                .Subscribe(x =>
                {
                    descriptionLabel.text = x;
                });
        }
        public void OnInventoryBtnPressed()
        {
            isOpened = !isOpened;

            gridManagers.gameObject.SetActive(isOpened);
            back.gameObject.SetActive(isOpened);
            useBtn.gameObject.SetActive(isOpened);
            holdBtn.gameObject.SetActive(isOpened);
            throwBtn.gameObject.SetActive(isOpened);
            descriptions.gameObject.SetActive(isOpened);
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
        private void initGrids()
        {
            for (int i = 0; i < iinventroy.Capacity; i++)
            {
                var grid_gameobject = GameObject.Instantiate(gridPrefab, gridManagers);
                grid_gameobject.transform.name = "Grid" + i;
                grids.Add(grid_gameobject);

                var gridScript = grid_gameobject.GetComponent<GridHandleView>();
                gridScripts.Add(gridScript);
                gridScript.Active(this, i);
                ClearGridContent(i);
            }

            
        }
        private void initDescrptionWidge()
        {
            descriptionWidge = transform
                .Find("DescriptionWidge")
                .GetComponent<InventoryDescriptionWidge>();
                //GetComponentInChildren<InventoryDescriptionWidge>();
            Assert.IsNotNull(descriptionWidge, "descriptionWidge is null at " + name);
            descriptionWidge.init(this);
        }
        private void asserts()
        {
            Assert.IsNotNull(descriptionLabel);
            Assert.IsNotNull(useBtn);
            Assert.IsNotNull(holdBtn);
            Assert.IsNotNull(throwBtn);
        }
        private void getReference(IInventorySystem inventorySystem)
        {
            iinventroy = inventorySystem;
            gridManagers = transform.Find("GridsManager");
            back = transform.Find("Background");
            descriptionLabel = transform
                .Find("DescriptionWidge")
                .Find("DescriptionLabel")
                .GetComponent<Text>();
            useBtn = transform.Find("UseBtn").GetComponent<Button>();
            holdBtn = transform.Find("HoldBtn").GetComponent<Button>();
            throwBtn = transform.Find("ThrowBtn").GetComponent<Button>();
            descriptions = transform.Find("DescriptionWidge");
        }
        #endregion


    }
}
