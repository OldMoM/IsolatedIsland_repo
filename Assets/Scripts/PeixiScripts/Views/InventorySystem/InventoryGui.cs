using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;
using UnityEngine.Assertions;


namespace Peixi
{
    public class InventoryGui : MonoBehaviour, IInventoryGui
    {
        public GameObject gridPrefab;
        List<GameObject> grids = new List<GameObject>();

        Transform back;
        Transform gridManagers;
        IInventorySystem iinventroy;

        bool isOpened = false;

        [SerializeField]
        [Tooltip("当前选中的Item")]
        private StringReactiveProperty selectedItem = new StringReactiveProperty("None");
        public string SelectedItem
        {
            get => selectedItem.Value;
            set
            {
                selectedItem.Value = value;
            }
        }
        public IObservable<string> OnSelectedItemChanged => selectedItem;
        public void InitModule(IInventorySystem inventorySystem)
        {
            iinventroy = inventorySystem;
            gridManagers = transform.Find("GridsManager");
            back = transform.Find("Background");

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
        }
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

                var gridScript = grid_gameobject.GetComponent<GridView>();
                gridScript.Active(this, i);
                ClearGridContent(i);
            }
        }
        public void OnInventoryBtnPressed()
        {
            isOpened = !isOpened;

            gridManagers.gameObject.SetActive(isOpened);
            back.gameObject.SetActive(isOpened);
        }
        public void OnPointerEnterGrid(int gridSerial)
        {
            selectedItem.Value = iinventroy.GetGridData(gridSerial).Item1;
        }
        public void OnPointerExitGrid(int gridSerial)
        {
            selectedItem.Value = "None";
        }
    }
}
