using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Peixi
{
    /// <summary>
    ///   <para>当背包使用消耗品时，由ArbitorSystem调用，激发相应消耗品效果</para>
    /// </summary>
    public static class UseSupplyService 
    {
        public static void UseApple(IPlayerPropertySystem property)
        {
            property.ChangeSatiety(15);
        }
    }
}
