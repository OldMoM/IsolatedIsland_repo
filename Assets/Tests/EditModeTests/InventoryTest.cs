using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Peixi;
using UniRx;
namespace Tests
{
    public class InventoryTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void AddItem_ThePointedItemDoesntExit_iron_true()
        {
            var inventory = new InventoryPresenter();
            inventory.AddItem("iron",2);
            var hasIron = inventory.HasItem("iron");
            Assert.IsTrue(hasIron);
        }
        [Test]
        public void AddItem_ThePointedItemHasBeenExisted_5()
        {
            var inventory = new InventoryPresenter();
            inventory.AddItem("iron", 2);
            inventory.AddItem("iron", 3);
            Assert.AreEqual(5, inventory.GetAmount("iron"));
        }
        [Test]
        public void GetAmount_ItemExisted_6and2()
        {
            var inventory = new InventoryPresenter(9);
            inventory
                .AddItem("iron", 6)
                .AddItem("copper", 2);

            var ironAmount = inventory.GetAmount("iron");
            var copperAmount = inventory.GetAmount("copper");

            Assert.AreEqual(6, ironAmount);
            Assert.AreEqual(2, copperAmount);
        }
        [Test]
        public void GetAmount_ItemNotExisted_0()
        {
            var inventory = new InventoryPresenter(9);
          
            var ironAmount = inventory.GetAmount("iron");
            Assert.AreEqual(0, ironAmount);
        }
        [Test]
        public void RemoveItem_removeAmountLessItemAmount_2and4()
        {
            var inventory = new InventoryPresenter(9);
            inventory.AddItem("iron", 5)
            .AddItem("copper", 12);
            Assert.AreEqual(5, inventory.GetAmount("iron"));
            Assert.AreEqual(12, inventory.GetAmount("copper"));

            inventory.RemoveItem("iron", 3);
            inventory.RemoveItem("copper", 8);
     
            Assert.AreEqual(2, inventory.GetAmount("iron"));
            Assert.AreEqual(4, inventory.GetAmount("copper"));
        }
        [Test]
        public void RemoveItem_removeAmountEqualsItemAmount_0()
        {
            var inventory = new InventoryPresenter();
            var hasOperated = inventory
                .AddItem("iron", 5)
                .RemoveItem("iron", 5);

            Assert.AreEqual(0, inventory.GetAmount("iron"));
            Assert.IsTrue(hasOperated);
          
        }
        [Test]
        public void RemoveItem_removeAmountLessItemAmount_false()
        {
            var inventory = new InventoryPresenter(9);
            var hasOperated = inventory
                .AddItem("iron", 5)
                .RemoveItem("iron", 7);

            Assert.IsFalse(hasOperated);
        }
        [Test]
        public void OnItemAdded_getmsg()
        {
            var inventory = new InventoryPresenter(9);

            inventory.OnInventoryChanged
                .Subscribe(x =>
                {
                    Assert.AreEqual("iron", x.NewValue.Name);
                    Assert.AreEqual(5, x.NewValue.Amount);
                });

            inventory.AddItem("iron", 5);
        }
        [Test]
        public void OnItemRemoved_getmsg()
        {
            var inventory = new InventoryPresenter();
            inventory.AddItem("iron", 5);

            inventory.OnInventoryChanged
                .Subscribe(x =>
                {
                    Assert.AreEqual("", x.NewValue.Name);
                    Assert.IsTrue(x.NewValue.IsEmpty);
                });
            inventory.RemoveItem("iron", 5);
        }
        [Test]
        public void GetItemData_ironAnd5_()
        {
            InventorySystem iinventory = GameObject.FindObjectOfType<InventorySystem>();
            var inventory = new InventoryPresenter(iinventory);
            inventory.AddItem("iron", 5);

            var data = inventory.GetItemData(0);
            Assert.AreEqual("iron", data.Item1);
            Assert.AreEqual(5, data.Item2);
        }
        [Test]
        public void GetItemData_None()
        {
            InventorySystem iinventory = GameObject.FindObjectOfType<InventorySystem>();
            var inventory = new InventoryPresenter(iinventory);
            inventory.AddItem("iron", 5);

            var data = inventory.GetItemData(1);
            Assert.AreEqual("None", data.Item1);
        }
    }
}
