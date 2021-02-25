using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UniRx;

namespace Peixi
{
    public class InGameUIComponentInterface : MonoBehaviour,IInGameUIComponentsInterface
    {
        public List<GameObject> childUICompoents;

        private IInventoryGui inventoryGui;

        public IInventoryGui InventoryGui
        {
            get
            {
                if (inventoryGui is null)
                {
                    inventoryGui = childUICompoents[2].GetComponent<InventoryGui>();
                }
                return inventoryGui;
            }
        }
    }
}
