using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

namespace Peixi
{
    public interface IInventorySystem
    {
        /// <summary>
        /// 查询背包是否有指定的Item
        /// </summary>
        /// <param name="name">Item的名字</param>
        /// <returns></returns>
        bool HasItem(string name);
        /// <summary>
        /// 查询背包中Item的数量
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        int GetAmount(string name);
        /// <summary>
        /// 移除背包中的Item
        /// </summary>
        /// <param name="name"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        bool RemoveItem(string name, int amount = 1);
        /// <summary>
        /// 向背包中添加Item
        /// </summary>
        /// <param name="name"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        InventoryPresenter AddItem(string name, int amount = 1);
        /// <summary>
        /// 当修改背包中Item数据被修改时触发此事件
        /// </summary>
        IObservable<CollectionReplaceEvent<InventoryGridData>> OnInventoryChanged { get; }
        /// <summary>
        /// 背包容量上限
        /// </summary>
        int Capacity { get; }
        /// <summary>
        /// 背包当前装载量
        /// </summary>
        int Load { get; set; }
        /// <summary>
        /// 获取背包格子中的数据
        /// </summary>
        /// <param name="gridSerial">背包格子编号</param>
        /// <returns></returns>
        ValueTuple<string, int> GetGridData(int gridSerial);
        /// <summary>
        /// 指针当前选中的Item
        /// </summary>
        string SelectedItem { get; set; }
        /// <summary>
        /// 指针当前选中的Item改变时触发此事件，并传递改变了的Item的name
        /// </summary>
        IObservable<string> OnSelectedItemChanged { get; }
        /// <summary>
        /// 当指针进入格子时，GridView脚本调用此方法
        /// </summary>
        /// <param name="gridSerial">格子编号</param>
        void OnPointerEnterGrid(int gridSerial);
        /// <summary>
        /// 当指针离开格子时，GridView脚本调用此方法
        /// </summary>
        /// <param name="gridSerial">格子编号</param>
        void OnPointerExitGrid(int gridSerial);

        IObservable<bool> OnOpenStateChanged { get; }
        IObservable<string> OnItemUsed { get; }
    }
}
