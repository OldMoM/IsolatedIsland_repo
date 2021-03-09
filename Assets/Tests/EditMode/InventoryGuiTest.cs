using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Peixi;
using UniRx;

namespace Tests
{
    public class InventoryGuiTest
    {
        private IInventoryGui Config()
        {
            IInventoryGui inventoryGui = new GameObject().AddComponent<InventoryGui>();
            return inventoryGui;
        }
        // A Test behaves as an ordinary method
        [Test]
        public void OnInventoryOpened()
        {
            var gui = Config();
            gui.OnOpenStateChanged
                .Where(x => x)
                .Subscribe(x =>
                {
                    Debug.Log("open inventory by gui");
                    Assert.IsTrue(x);
                });

            gui.SetActive(true);
        }

    
    }
}
