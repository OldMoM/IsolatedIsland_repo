using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace Peixi
{
    /// <summary>
    /// 将InventorySystem的各个组件用适配器模式进行组装
    /// </summary>
    public class InventorySystem : MonoBehaviour, IInventorySystem
    {
        InventoryPresenter presenter;
        InventoryGui gui;

        public InventorySetting setting;

        #region//implement interface
        public IObservable<CollectionReplaceEvent<InventoryGridData>> OnInventoryChanged => presenter.OnInventoryChanged;
        public int Capacity => setting.capacity;
        public int Load
        {
            get => setting.load;
            set
            {
                setting.load = value;
            }
        }
        public string SelectedItem { get => gui.SelectedItem; set => throw new NotImplementedException(); }
        public IObservable<string> OnSelectedItemChanged => gui.OnSelectedItemChanged;
        public InventoryPresenter AddItem(string name, int amount = 1)
        {
            return presenter.AddItem(name, amount);
        }
        public int GetAmount(string name)
        {
            return presenter.GetAmount(name);
        }
        public bool HasItem(string name)
        {
            return presenter.HasItem(name);
        }
        public bool RemoveItem(string name, int amount = 1)
        {
            return presenter.RemoveItem(name, amount);
        }
        public ValueTuple<string, int> GetGridData(int gridSerial)
        {
            return presenter.GetItemData(gridSerial);
        }
        public void OnPointerEnterGrid(int gridSerial)
        {
            //gui.OnPointerEnterGrid(gridSerial);
        }
        public void OnPointerExitGrid(int gridSerial)
        {
            gui.OnPointerExitGrid(gridSerial);
        }
        #endregion
        public void Init()
        {
            presenter = new InventoryPresenter(this);
            gui = FindObjectOfType<InventoryGui>();
            Assert.IsNotNull(gui, "Failed to find InventoryGui script in Hierarchy");
            gui.init(this);
        }
        private void OnEnable()
        {
            Init();
        }
    }
    [Serializable]
    public struct InventorySetting
    {
        public int capacity;
        public int load;
        public Vector2 gridSize;
        public int gridSpaceInPixel;
    }
}
