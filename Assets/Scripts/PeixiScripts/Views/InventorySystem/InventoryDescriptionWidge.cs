using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.Assertions;
using UnityEngine.U2D;

namespace Peixi
{
    public class InventoryDescriptionWidge : MonoBehaviour
    {
        [SerializeField] private Transform text_tran;
        [SerializeField] private Transform icon_tran;
        [SerializeField] private SpriteAtlas atlas;

        private Text description;
        private Image icon;
        private IInventoryGui _igui;

        public InventoryDescriptionWidge init(IInventoryGui gui)
        {
            _igui = gui;

            description = text_tran.GetComponent<Text>();
            icon = icon_tran.GetComponent<Image>();

            Assert.IsNotNull(description, "description is null");
            Assert.IsNotNull(icon, "icon is null");

            gui.OnSelectedItemChanged
                .Subscribe(x =>
                {
                    if ( x == "None" || x=="")
                    {
                        description.text = string.Empty;
                        icon.sprite = null;
                        icon.color = Color.clear;
                    }
                    else
                    {
                        //从ItemDescriptions读取物品描述@wip
                        description.text = ItemDescriptions.des[x];

                        //从Altlas读取物品Icon
                        icon.color = Color.white;
                        icon.sprite = atlas.GetSprite(x);
                    }
                });
            return this;
        }
    }
}
