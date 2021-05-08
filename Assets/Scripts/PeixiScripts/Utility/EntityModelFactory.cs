using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Peixi
{

    /// <summary>创建Entity的Model</summary>
    public static class EntityModelFactory
    {
        /// <summary>
        ///   <para>默认isActive = true</para>
        ///   <para>需要显示声明</para>
        ///   <para>onDayStart</para>
        ///   <para>onDayEnd</para>
        ///   <para>attachedObject</para>
        /// </summary>
        /// <returns>
        ///   <br />
        /// </returns>
        public static IslandModel CreateIslandModel()
        {
            var model = new IslandModel();
            model.isActive = true;
            model.durability = new IntReactiveProperty();
            model.onIslandDestoryed = new Subject<Vector2Int>();

            return model;
        }
    }
}
