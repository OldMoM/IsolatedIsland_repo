using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Peixi
{
    public class InventoryDescriptionWidge : MonoBehaviour
    {
        private Text description;
        private RawImage icon;
        private IInventoryGui _igui;

        public InventoryDescriptionWidge init(IInventoryGui gui)
        {
            _igui = gui;

            description = GetComponentInChildren<Text>();
            icon = GetComponentInChildren<RawImage>();
            gui.OnSelectedItemChanged
                .Subscribe(x =>
                {
                    description.text = x;
                });
            return this;
        }
    }
}
