using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Peixi
{
    public class InventorySystem : MonoBehaviour,IInventorySystem
    {
        InventoryPresenter inventory;
        public InventorySetting setting;

        #region//implement interface
        public IObservable<CollectionReplaceEvent<InventoryGridData>> OnInventoryChanged => inventory.OnInventoryChanged;

        public int Capacity => setting.capacity;
        public int Load 
        { 
            get => setting.load;
            set
            {
                setting.load = value;
            }
        }

        public InventoryPresenter AddItem(string name, int amount = 1)
        {
            return inventory.AddItem(name, amount);
        }
        public int GetAmount(string name)
        {
            return inventory.GetAmount(name);
        }

        public bool HasItem(string name)
        {
            return inventory.HasItem(name);
        }
        public bool RemoveItem(string name, int amount = 1)
        {
            return inventory.RemoveItem(name, amount);
        }
        public ValueTuple<string, int> GetGridDate(int gridSerial)
        {
            return inventory.GetItemData(gridSerial);
        }
        #endregion
        private void Awake()
        {
            inventory = new InventoryPresenter(this);
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
