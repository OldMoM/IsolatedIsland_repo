using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;
using System;
using UnityEngine.Assertions;

namespace Peixi
{
    /// <summary>
    ///   <para>基础设施层：只提供查询，写入和读取操作</para>
    ///   <para>
    ///     <br />
    ///   </para>
    /// </summary>
    /// <remarks>构造函数启动改为：提供Init方法，手段启动，增加其可测试性@wip</remarks>
    public class InventoryCorePresenter 
    {
        private InventoryModel<InventoryGridData> model = new InventoryModel<InventoryGridData>();
        private IInventorySystem m_inventorySystem;

        public int gridCapacity = 9;

        public IObservable<CollectionReplaceEvent<InventoryGridData>> OnInventoryChanged => model.set.ObserveReplace();

        [Obsolete]
        /// <summary>
        /// This one is used for game
        /// </summary>
        /// <param name="system"></param>
        public InventoryCorePresenter(IInventorySystem system)
        {
            m_inventorySystem = system;

            for (int i = 0; i < system.Capacity; i++)
            {
                var _gridData = new InventoryGridData(i, "None", 0, true);
                model.set.Add(_gridData);
            }
        }
        [Obsolete]
        /// <summary>
        /// This one is used for test
        /// </summary>
        /// <param name="capacity"></param>
        public InventoryCorePresenter(int capacity = 6)
        {
            for (int i = 0; i < capacity; i++)
            {
                var _gridData = new InventoryGridData(i, "", 0, true);
                model.set.Add(_gridData);
            }
        }
        public InventoryCorePresenter() { }


        public void Init()
        {
            //创建核心数据

            //断言


            //初始创建9个数据格
            for (int i = 0; i < gridCapacity; i++)
            {
                var _gridData = new InventoryGridData(i, "", 0, true);
                model.set.Add(_gridData);
            }
        }

        /// <summary>Adds the item.</summary>
        /// <param name="name">The name.</param>
        /// <param name="amount">The amount.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <remarks>-在添加Item时需要经过添加规则判断，详见AddItemAgent</remarks>
        /// <seealso cref="AddItemAgent" />
        public InventoryCorePresenter AddItem(string name,int amount=1)
        {
            var searchGrid = model.set
                 .ToObservable();

            var itemInfo = GetItemPosition(name);


            if (itemInfo.Item1)
            {
                var _position = itemInfo.Item2;
                var data_temp = model.set[_position];
                data_temp.Amount += amount;
                model.set[_position] = data_temp;
            }
            else
            {
                searchGrid
                .Where(x => x.IsEmpty)
                .Take(1)
                .Subscribe(y =>
                {
                    var data_temp = y;
                    data_temp.Amount += amount;
                    data_temp.Name = name;
                    data_temp.IsEmpty = false;
                    model.set[data_temp.Position] = data_temp;

                    //m_inventorySystem.Load++;
                });
            }
            return this;
        }
        [Obsolete]
        public bool HasItem(string name)
        {
            bool hasItem = false;
            model.set
                .ToObservable()
                .Where(x => x.Name == name)
                .Subscribe(y =>
                {
                    hasItem = true;
                });
            return hasItem;
        }
        public int GetAmount(string name)
        {
            int amount = 0;
            model.set
             .Where(x => x.Name == name)
             .ToObservable()
             .Subscribe(y => 
             { 
                 amount = y.Amount;
             });
            return amount;
        }
        public bool RemoveItem(string name,int amount=1)
        {
            bool operated = true;
            var gridInfo = GetItemPosition(name);
            Assert.IsTrue(gridInfo.Item1, name + " doesn't exist in backpack");
            var position = gridInfo.Item2;
            var data = model.set[gridInfo.Item2];

            if (data.Amount > amount)
            {
                data.Amount -= amount; 
            }
            else if (data.Amount == amount)
            {
                //Debug.Log("clear grid");
                data.Amount = 0;
                data.Name = "";
                data.IsEmpty = true;
            }
            else
            {
                operated = false;
            }

            model.set[position] = data;
            return operated;
        }
        private ValueTuple<bool,int> GetItemPosition(string name)
        {
            ValueTuple<bool, int> info = new ValueTuple<bool, int>(false, 0);
            info.Item1 = false;
            model.set
                .ToObservable()
                .Where(x => x.Name == name)
                .Subscribe(y =>
                {
                    info.Item1 = true;
                    info.Item2 = y.Position;
                });

            return info;
        }
        public ValueTuple<string, int> GetItemData(int gridSerial)
        {
            var count = model.set.Count;
            Assert.IsTrue(count > gridSerial, "");

            var itemData = new ValueTuple<string, int>("None", 0);
            var items = model.set.ToList();
            itemData.Item1 = items[gridSerial].Name;
            itemData.Item2 = items[gridSerial].Amount;
            return itemData;
        }
    }
}

