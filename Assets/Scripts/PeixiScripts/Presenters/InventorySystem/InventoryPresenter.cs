using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;
using System;
using UnityEngine.Assertions;

namespace Peixi
{
    public class InventoryPresenter 
    {
        private InventoryModel<InventoryGridData> inventory = new InventoryModel<InventoryGridData>();
        private IInventorySystem m_inventorySystem;
        public IObservable<CollectionReplaceEvent<InventoryGridData>> OnInventoryChanged => inventory.inventoryData.ObserveReplace();

        /// <summary>
        /// This one is used for game
        /// </summary>
        /// <param name="system"></param>
        public InventoryPresenter(IInventorySystem system)
        {
            m_inventorySystem = system;

            for (int i = 0; i < system.Capacity; i++)
            {
                var _gridData = new InventoryGridData(i, "None", 0, true);
                inventory.inventoryData.Add(_gridData);
            }
        }
        /// <summary>
        /// This one is used for test
        /// </summary>
        /// <param name="capacity"></param>
        public InventoryPresenter(int capacity = 6)
        {
            for (int i = 0; i < capacity; i++)
            {
                var _gridData = new InventoryGridData(i, "", 0, true);
                inventory.inventoryData.Add(_gridData);
            }
        }
        public InventoryPresenter AddItem(string name,int amount=1)
        {
            var searchGrid = inventory.inventoryData
                 .ToObservable();

            var itemInfo = GetItemPosition(name);


            if (itemInfo.Item1)
            {
                var _position = itemInfo.Item2;
                var data_temp = inventory.inventoryData[_position];
                data_temp.Amount += amount;
                inventory.inventoryData[_position] = data_temp;
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
                    inventory.inventoryData[data_temp.Position] = data_temp;

                    m_inventorySystem.Load++;
                });

                
            }
            return this;
        }
        public bool HasItem(string name)
        {
            bool hasItem = false;
            inventory.inventoryData
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
            inventory.inventoryData
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
            var data = inventory.inventoryData[gridInfo.Item2];

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

            inventory.inventoryData[position] = data;
            return operated;
        }
        private ValueTuple<bool,int> GetItemPosition(string name)
        {
            ValueTuple<bool, int> info = new ValueTuple<bool, int>(false, 0);
            info.Item1 = false;
            inventory.inventoryData
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
            var count = inventory.inventoryData.Count;
            Assert.IsTrue(count > gridSerial, "");

            var itemData = new ValueTuple<string, int>("None", 0);
            var items = inventory.inventoryData.ToList();
            itemData.Item1 = items[gridSerial].Name;
            itemData.Item2 = items[gridSerial].Amount;
            return itemData;
        }
    }
}

