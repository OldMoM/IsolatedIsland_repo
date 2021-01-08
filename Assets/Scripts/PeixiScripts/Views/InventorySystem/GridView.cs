using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using UnityEngine.Assertions;

namespace Peixi
{
    /// <summary>
    /// 向InventoryGui告知玩家对Grid的图形操作
    /// </summary>
    public class GridView : MonoBehaviour
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
        }
        public void Active(IInventoryGui _inventoryGui, int _gridSerial)
        {
            iinventoryGui = _inventoryGui;
            gridSerial = _gridSerial;
        }
    }
}
