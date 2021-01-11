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

        Transform back;
        Transform gridManagers;
        Transform descriptions;
        Text descriptionLabel;
        Button useBtn;
        Button holdBtn;
        Button throwBtn;


        IInventorySystem iinventroy;

        bool isOpened = false;

        [SerializeField]
        [Tooltip("当前选中的Item")]
        private StringReactiveProperty selectedItem = new StringReactiveProperty("None");
        [SerializeField]
        [Tooltip("指针点击的Item")]
        private StringReactiveProperty clickedItem = new StringReactiveProperty("None");
        #endregion

        public void InitModule(IInventorySystem inventorySystem)
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

            Assert.IsNotNull(descriptionLabel);
            Assert.IsNotNull(useBtn);
            Assert.IsNotNull(holdBtn);
            Assert.IsNotNull(throwBtn);

            InitGrids();

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

        #region//Implementation of interface
        public string SelectedItem
        {
            get => selectedItem.Value;
            set
            {
                selectedItem.Value = value;
            }
        }
        public IObservable<string> OnSelectedItemChanged => selectedItem;
        public string ClickedItem => clickedItem.Value;
        public IObservable<string> OnClickedItemChanged => clickedItem;
        private void SetGridContent(int gridNum, string name, int amount)
        {
            var grid = grids[gridNum];
            Assert.IsNotNull(grid);
            Text text = grid.transform.GetComponentInChildren<Text>();
            Assert.IsNotNull(text);
            text.text = name + ": " + amount;
        }
        private void ClearGridContent(int gridNum)
        {
            var grid = grids[gridNum];
            Text text = grid.transform.GetComponentInChildren<Text>();
            text.text = "";
        }
        private void InitGrids()
        {
            for (int i = 0; i < iinventroy.Capacity; i++)
            {
                var grid_gameobject = GameObject.Instantiate(gridPrefab, gridManagers);
                grid_gameobject.transform.name = "Grid" + i;
                grids.Add(grid_gameobject);

                var gridScript = grid_gameobject.GetComponent<GridHandleView>();
                gridScript.Active(this, i);
                ClearGridContent(i);
            }
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
        public void OnPointerEnterGrid(int gridSerial)
        {
            selectedItem.Value = iinventroy.GetGridData(gridSerial).Item1;
        }
        public void OnPointerExitGrid(int gridSerial)
        {
            selectedItem.Value = "None";
        }
        public void OnPointerClickGrid(int gridSerial)
        {
            clickedItem.Value = iinventroy.GetGridData(gridSerial).Item1;
        }
        #endregion
    }
}
