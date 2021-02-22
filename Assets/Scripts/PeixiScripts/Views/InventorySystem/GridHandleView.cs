using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using UnityEngine.Assertions;
using UnityEngine.U2D;

namespace Peixi
{
    [HelpURL("https://spicegamestudio.pingcode.com/wiki/spaces/5fd0cd48fee2547e79b1b100/pages/5ff9677589088414ae232466")]
    /// <summary>
    /// 处理玩家对Grid的GUI操作
    /// </summary>
    public class GridHandleView : MonoBehaviour
    {
        private Button button;
        private Image icon;
        private Text amount;
        IInventoryGui iinventoryGui;

        public Transform icon_transform;

        public int gridSerial 
        { 
            get=> gridModel.gridSerial;
            set 
            {
                gridModel.gridSerial = value;
            }
        } 
        public string itemName 
        { 
            get => gridModel.itemName;
            set
            {
                gridModel.itemName = value;
                icon.sprite = atlas.GetSprite(value);
                icon.color = new Color(1, 1, 1, 1);
            }
        } 
        public int itemAmount 
        {
            get => gridModel.amount;
            set 
            {
                if (value > 0)
                {
                    amount.text = "×" + value.ToString();
                    gridModel.amount = value;
                }
            }
        } 

        public GridModel gridModel;
        public SpriteAtlas atlas;

        public void Active(IInventoryGui _inventoryGui, int _gridSerial)
        {
            iinventoryGui = _inventoryGui;
            gridSerial = _gridSerial;

            var button_transform = transform.Find("Button");
            var icon_tranform = transform.Find("Icon");
            var content_transform = transform.Find("Content");

            button = GetComponentInChildren<Button>();
            icon = icon_tranform.GetComponent<Image>();
            amount = GetComponentInChildren<Text>();

            Assert.IsNotNull(icon);
            Assert.IsNotNull(button);
            Assert.IsNotNull(amount);

            button.OnPointerEnterAsObservable()
                .Subscribe(x =>
                {
                    iinventoryGui.OnPointerEnterGrid(gridSerial, itemName);
                });
            button.OnPointerExitAsObservable()
                .Subscribe(x =>
                {
                    iinventoryGui.OnPointerExitGrid(gridSerial);
                });
            button.OnPointerClickAsObservable()
                .Subscribe(x =>
                {
                    iinventoryGui.OnPointerClickGrid(gridSerial,itemName);
                });
        }

        public void clearGrid()
        {
            amount.text = string.Empty;
            icon.color = new Color(1, 1, 1, 0);
            gridModel.amount = 0;
            gridModel.itemName = string.Empty;
        }
    }
    [System.Serializable]
    public struct GridModel
    {
        public string itemName;
        public int amount;
        public int gridSerial;
    }
}
