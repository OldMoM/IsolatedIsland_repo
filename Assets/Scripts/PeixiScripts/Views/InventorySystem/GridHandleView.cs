using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using UnityEngine.Assertions;

namespace Peixi
{
    [HelpURL("https://spicegamestudio.pingcode.com/wiki/spaces/5fd0cd48fee2547e79b1b100/pages/5ff9677589088414ae232466")]
    /// <summary>
    /// 处理玩家对Grid的GUI操作
    /// </summary>
    public class GridHandleView : MonoBehaviour
    {
        private Button button;
        IInventoryGui iinventoryGui;
        public int gridSerial;
        // Start is called before the first frame update
        void Start()
        {
            button = GetComponentInChildren<Button>();
            button.OnPointerEnterAsObservable()
                .Subscribe(x =>
                {
                    iinventoryGui.OnPointerEnterGrid(gridSerial);
                });
            button.OnPointerExitAsObservable()
                .Subscribe(x =>
                {
                    iinventoryGui.OnPointerExitGrid(gridSerial);
                });
            button.OnPointerClickAsObservable()
                .Subscribe(x =>
                {
                    iinventoryGui.OnPointerClickGrid(gridSerial);
                });   
        }
        public void Active(IInventoryGui _inventoryGui, int _gridSerial)
        {
            iinventoryGui = _inventoryGui;
            gridSerial = _gridSerial;
        }
    }
}
