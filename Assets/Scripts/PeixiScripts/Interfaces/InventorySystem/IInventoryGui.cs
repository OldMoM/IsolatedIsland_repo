using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
namespace Peixi
{
    public interface IInventoryGui 
    {
        /// <summary>
        /// 指针当前选中的Item
        /// </summary>
        string SelectedItem { get; set; }
        /// <summary>
        /// 指针当前选中的Item改变时触发此事件
        /// </summary>
        IObservable<string> OnSelectedItemChanged { get; }

        void OnPointerEnterGrid(int gridSerial);

        void OnPointerExitGrid(int gridSerial);
    }
}
