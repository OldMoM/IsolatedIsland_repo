using Peixi;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Peixi
{
    public class ThirstAgent
    {
        private AgentDependency dependency;

        public ThirstAgent(AgentDependency Dependency)
        {
            dependency = Dependency;

            var thirstReduceRate = GameConfig.Singleton.InteractionConfig["playerThirstReduceInterval"];
            //每四秒口渴值减1
            Observable.Interval(TimeSpan.FromSeconds(thirstReduceRate))
               .Subscribe(x =>
               {
                   //Debug.Log("Thirst is:" + dependency.playerPropertySystem.Thirst);
                   dependency.playerPropertySystem.ChangeThirst(-1);
               });

            //根据口渴值设置口渴等级
            //你之前写成OnSatietyChanged，这传递的是饱腹感值
            dependency.playerPropertySystem.OnThirstChanged
                .Subscribe(x =>
                {
                    if (x > 0 && x < 70)
                    {
                        dependency.playerPropertySystem.ThirstLevel = PropertyLevel.Euclid;
                    }

                    if (x == 0)
                    {
                        dependency.playerPropertySystem.ThirstLevel = PropertyLevel.Keter;

                        AudioEvents.StartAudio("OnPlayerGetExtremeThirsty");
                    }

                    if(x > 70)
                    {
                        dependency.playerPropertySystem.ThirstLevel = PropertyLevel.Safe;
                    }
                });

            dependency.playerPropertySystem.OnThirstChanged
                .Where(x=>x==0)
                .Subscribe(x =>
                {
                        dependency.speed = 2;
                });
        }
    }
}
