using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Peixi
{
    /// <summary>
    ///   <para>当背包使用消耗品时，由ArbitorSystem调用，激发相应消耗品效果</para>
    /// </summary>
    public static class UseSupplyService 
    {
        private static Dictionary<string, Action> useSuppleActions = new Dictionary<string, Action>()
        {
            {"water", ()=>
                {
                    var playerProperty = InterfaceArichives.Archive.IPlayerPropertySystem;
                    var preThirst = playerProperty.Thirst;
                    playerProperty.ChangeThirst(10);
                    Debug.Log("互动成功，先前Thirst为"+preThirst+"现在为"+ playerProperty.Thirst);
                }
            },

            {
                "fruit",()=>{
                    var playerProperty = InterfaceArichives.Archive.IPlayerPropertySystem;
                    playerProperty.ChangeSatiety(10);
                     Debug.Log("It's successful interaction message. Player Satiety now is "+ playerProperty.Satiety);
                }
            },

            {
                "grilledFish",()=>
                {
                    var playerProperty = InterfaceArichives.Archive.IPlayerPropertySystem;
                    var preSatiety = playerProperty.Satiety;
                    playerProperty.ChangeSatiety(10);
                    Debug.Log("互动成功，先前Satiety为"+preSatiety+"现在为"+ playerProperty.Satiety);
                }
            }
        };
        public static void UseSupply(string item)
        {
            useSuppleActions[item]();
        }
    }
}
