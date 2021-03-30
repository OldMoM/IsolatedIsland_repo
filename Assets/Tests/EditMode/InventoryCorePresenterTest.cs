using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Peixi;

namespace Tests
{
    public class InventoryCorePresenterTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void 空背包_添加1份string_1()
        {
            var core = new InventoryCorePresenter();
            core.Init();

            core.AddItem("string");

            var stringAmount = core.GetAmount("string");

            Assert.AreEqual(0, stringAmount);
        }
    }
}
