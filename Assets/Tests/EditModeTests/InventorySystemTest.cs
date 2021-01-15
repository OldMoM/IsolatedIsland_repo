using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Peixi;
using UniRx;

namespace Tests
{
    public class InventorySystemTest
    {
        private IInventorySystem Config()
        {
            var iinventory = GameObject.FindObjectOfType<InventorySystem>();
            iinventory.Init();
            return iinventory;
        }
        // A Test behaves as an ordinary method
        [Test]
        public void InventorySystem_HasItem_true()
        {
            var iinventory = Config();
            iinventory.AddItem("iron", 3);
            var hasIron = iinventory.HasItem("iron");
            Assert.IsTrue(hasIron);
        }
        [Test]
        public void InventorySystem_RemoveItem_1and2()
        {
            var inventory = Config();
            inventory.AddItem("iron", 3);
            inventory.AddItem("copper", 3);

            inventory.RemoveItem("iron", 2);
            inventory.RemoveItem("copper", 1);

            var ironAmount = inventory.GetAmount("iron");
            var copperAmount = inventory.GetAmount("copper");
            Assert.AreEqual(1, ironAmount);
            Assert.AreEqual(2, copperAmount);
        }
        [Test]
        public void InventorySystem_OnItemAdded_iron()
        {
            var inventory = Config();
            inventory.OnInventoryChanged
                .Subscribe(x =>
                {
                    Assert.AreEqual("iron", x.NewValue.Name);
                });
            inventory.AddItem("iron", 3);
        }
    }
}
