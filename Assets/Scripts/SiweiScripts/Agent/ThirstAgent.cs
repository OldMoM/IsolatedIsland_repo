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

            //每四秒口渴值减1
            Observable.Interval(TimeSpan.FromSeconds(4))
               .Subscribe(x =>
               {
                   Debug.Log("Thirst is:" + dependency.playerPropertySystem.Thirst);
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

                    else if (x == 0)
                    {
                        dependency.playerPropertySystem.ThirstLevel = PropertyLevel.Keter;
                    }

                    else // (x >= 70)
                    {
                        dependency.playerPropertySystem.ThirstLevel = PropertyLevel.Safe;
                    }
                });

            dependency.playerPropertySystem.OnThirstChanged
                .Subscribe(x =>
                {
                    if (x == 0)
                    {
                        dependency.speed = 2;
                    }
                });
        }
    }
}
