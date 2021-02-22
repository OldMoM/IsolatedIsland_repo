using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Peixi;
using UniRx;

namespace Tests
{
    public class ArbitorEyeModuleTest:ArbitorEyeModule
    {
        /// <summary>
        /// 查询Item是否已经被压入quene中了
        /// </summary>
        [Test]
        public void ArbitorEyeModule_HasItem_true()
        {
            var itemObject_test = new GameObject();
            var baseItem = itemObject_test.AddComponent<BaseItem>();

            var arbitorEye = new ArbitorEyeModule();
            arbitorEye.AddItemToPendingQuene(baseItem);

            Assert.IsTrue(arbitorEye.HasItem(baseItem));
        }
        [Test]
        public void ArbitorEyeModule_HasItem_false()
        {
            var itemObject_test1 = new GameObject();
            var baseItem1 = itemObject_test1.AddComponent<BaseItem>();

            var itemObject_test2 = new GameObject();
            var baseItem2 = itemObject_test2.AddComponent<BaseItem>();

            var arbitorEye = new ArbitorEyeModule();
            arbitorEye.AddItemToPendingQuene(baseItem1);

            Assert.IsFalse(arbitorEye.HasItem(baseItem2));
        }
        [Test]
        public void ArbitorEyeModule_AddTwoSameItem_1()
        {
            //arrange
            var itemObject_test1 = new GameObject();
            var baseItem1 = itemObject_test1.AddComponent<BaseItem>();

            var itemObject_test2 = new GameObject();
            var baseItem2 = itemObject_test2.AddComponent<BaseItem>();

            var arbitorEye = new ArbitorEyeModule();

            arbitorEye.OnQuenedCountChanged
            .Subscribe(x =>
            {
                Assert.AreEqual(1, x);
            });

            arbitorEye.AddItemToPendingQuene(baseItem1);
            arbitorEye.AddItemToPendingQuene(baseItem1);
        }
        [Test]
        public void ArbitorEyeModule_RemoveItemFromQuene_false()
        {
            var itemObject_test1 = new GameObject();
            var baseItem1 = itemObject_test1.AddComponent<BaseItem>();

            var itemObject_test2 = new GameObject();
            var baseItem2 = itemObject_test2.AddComponent<BaseItem>();

            var arbitorEye = new ArbitorEyeModule();
            arbitorEye.AddItemToPendingQuene(baseItem1);
            arbitorEye.AddItemToPendingQuene(baseItem2);

            arbitorEye.RemoveItemFromPendingQuene(baseItem1);
            Assert.IsFalse(arbitorEye.HasItem(baseItem1));
            Assert.AreEqual(1, arbitorEye.PendingItemCount);

        }
        //[Test]
        //public void GetCurrentHandleItem_OnItemAdded_SecondOne()
        //{
        //    var itemObject_test1 = new GameObject();
        //    Assert.IsNotNull(itemObject_test1);
        //    var baseItem1 = itemObject_test1.AddComponent<BaseItem>();
        //    Assert.IsNotNull(baseItem1);
        //    baseItem1.data.name = "iron";
        //    var hascode1 = baseItem1.GetHashCode();

        //    var itemObject_test2 = new GameObject();
        //    var baseItem2 = itemObject_test2.AddComponent<BaseItem>();
        //    baseItem2.data.name = "copper";
        //    var hascode2 = baseItem2.GetHashCode();

        //    var arbitorEye = new ArbitorEyeModule();
        //    arbitorEye.AddItemToPendingQuene(baseItem1);
        //    arbitorEye.AddItemToPendingQuene(baseItem2);

        //    arbitorEye.HandleItem
        //        .Subscribe(x =>
        //        {
        //            Assert.AreEqual(hascode2, x.GetHashCode());
        //        });
        //}
        //[Test]
        //public void GetCurrentHandleItem_OnItemRemoved_FirstOne()
        //{
        //    var itemObject_test1 = new GameObject();
        //    Assert.IsNotNull(itemObject_test1);
        //    var baseItem1 = itemObject_test1.AddComponent<BaseItem>();
        //    Assert.IsNotNull(baseItem1);
        //    baseItem1.data.name = "iron";
        //    var hascode1 = baseItem1.GetHashCode();

        //    var itemObject_test2 = new GameObject();
        //    var baseItem2 = itemObject_test2.AddComponent<BaseItem>();
        //    baseItem2.data.name = "copper";
        //    var hascode2 = baseItem2.GetHashCode();

        //    var arbitorEye = new ArbitorEyeModule();
        //    arbitorEye.AddItemToPendingQuene(baseItem1);
        //    arbitorEye.AddItemToPendingQuene(baseItem2);
        //    arbitorEye.RemoveItemFromPendingQuene(baseItem2);

        //    arbitorEye.OnQuenedCountChanged
        //        .Subscribe(x =>
        //        {
        //            Assert.AreEqual(1, x);
        //        });

        //    arbitorEye.HandleItem
        //        .Subscribe(x =>
        //        {
        //            Debug.Log(x.GetHashCode());
        //            Debug.Log(hascode1);
        //            Debug.Log(hascode2);
        //            Assert.AreEqual(hascode1, x.GetHashCode());
        //        });
        //}
        [Test]
        public void GetNearestItemHashCode_OnItemAdded_item1()
        {
            var itemObject_test1 = new GameObject();
            var baseItem1 = itemObject_test1.AddComponent<BaseItem>();
            itemObject_test1.transform.position = new Vector3(1, 1, 1);

            var itemObject_test2 = new GameObject();
            var baseItem2 = itemObject_test2.AddComponent<BaseItem>();
            itemObject_test2.transform.position = new Vector3(2, 2, 2);

            var arbitorEye = new ArbitorEyeModule();

            AddItemToPendingQuene(baseItem1);
            AddItemToPendingQuene(baseItem2);

            var nearestItemHashcode = GetNearestItemHashcode(Vector3.zero);
            Assert.AreEqual(baseItem1.GetHashCode(), nearestItemHashcode);
        }
        [Test]
        public void GetNearestItemHashCode_OnItemRemoved_item2() 
        {
            var itemObject_test1 = new GameObject();
            var baseItem1 = itemObject_test1.AddComponent<BaseItem>();
            itemObject_test1.transform.position = new Vector3(1, 1, 1);

            var itemObject_test2 = new GameObject();
            var baseItem2 = itemObject_test1.AddComponent<BaseItem>();
            itemObject_test2.transform.position = new Vector3(2, 2, 2);

            var itemObject_test3 = new GameObject();
            var baseItem3 = itemObject_test3.AddComponent<BaseItem>();
            itemObject_test3.transform.position = new Vector3(3, 3, 3);

            AddItemToPendingQuene(baseItem1);
            AddItemToPendingQuene(baseItem2);
            AddItemToPendingQuene(baseItem3);

            RemoveItemFromPendingQuene(baseItem1);
            var nearestItemHashcode = GetNearestItemHashcode(Vector3.zero);
            Assert.AreEqual(baseItem2.GetHashCode(), nearestItemHashcode);
        }

    }

}
