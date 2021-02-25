using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Peixi
{
    public struct PotionUseAgent
    {
        private IInventorySystem Inventory => InterfaceArichives.Archive.IInventorySystem;
        private IPlayerPropertySystem Property => InterfaceArichives.Archive.IPlayerPropertySystem;

        public void UsePotion(string name)
        {
            Inventory.RemoveItem(name);
            if (name == ItemTags.water)
            {
                
                Property.ChangeThirst(-5);
            }
            if (name == ItemTags.bread)
            {
                Property.ChangeHunger(-5);
            }
        }
    }
}
