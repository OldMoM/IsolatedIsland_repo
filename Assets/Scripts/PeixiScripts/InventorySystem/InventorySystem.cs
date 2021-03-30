using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace Peixi
{
    /// <summary>
    ///   <para>设计思路：背包的本质就是Crud操作</para>
    ///   <para>功能需求：使用Excel管理物品信息----MVP架构</para>
    ///   <para>                   背包数据要便于保存成json文件-----业务和数据分析</para>
    ///   <para>                   背包中有3个栏位容纳：材料，消耗品，重要物品</para>
    ///   <para>                   *背包有重量上线</para>
    ///   <para>构成模块：<br /></para>
    /// </summary>
    /// <seealso cref="InventoryGui" />
    public class InventorySystem : MonoBehaviour, IInventorySystem
    {
        InventoryCorePresenter presenter;
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

        public IObservable<bool> OnOpenStateChanged => throw new NotImplementedException();

        public IObservable<string> OnItemUsed => throw new NotImplementedException();

        public InventoryCorePresenter AddItem(string name, int amount = 1)
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
            presenter = new InventoryCorePresenter(this);
        }
        private void OnEnable()
        {
            Init();
        }
    }
}
