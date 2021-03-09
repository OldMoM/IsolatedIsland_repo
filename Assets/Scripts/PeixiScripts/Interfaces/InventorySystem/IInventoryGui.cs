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
        /// 指针点击的Item
        /// </summary>
        string ClickedItem { get;}
        /// <summary>
        /// 当前指针点击的Item发生变化时，触发此事件
        /// </summary>
        IObservable<string> OnClickedItemChanged { get; }
        /// <summary>
        /// 指针当前选中的Item改变时触发此事件
        /// </summary>
        IObservable<string> OnSelectedItemChanged { get; }
        void OnPointerEnterGrid(int gridSerial,string name);
        void OnPointerExitGrid(int gridSerial);
        /// <summary>
        /// 指针点击的格子是触发时，由GridView调用此事件
        /// </summary>
        /// <param name="gridSerial"></param>
        void OnPointerClickGrid(int gridSerial,string name);
        InventoryGui SetActive(bool active);
        IObservable<bool> OnOpenStateChanged { get; }
        IObservable<string> OnItemUsed { get; }
    }
}
