using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
namespace Peixi
{
    /// <summary>
    ///   <para>功能：</para>
    ///   <para>1.在装入Item时，如果背包容量不够，不能装入，返回false回调</para>
    ///   <para>2.在装入Item时，如果背包超重，不能装入，返回false回调</para>
    ///   <para>3.按照Item种类装入不同背包</para>
    ///   <para>4.当背包中有Item时，数量+1</para>
    ///   <para>  当背包中无Item时，创建个新数据@wip</para>
    /// </summary>
    public class AddItemAgent 
    {
        public List<InventoryGridData> data;
        public void AddItem()
        {

        }
    }
}
