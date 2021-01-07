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
        ValueTuple<string, int> GetGridDate(int gridSerial);
    }
}
