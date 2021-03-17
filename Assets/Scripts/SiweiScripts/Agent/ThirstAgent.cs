using Peixi;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;


namespace peixi
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
            dependency.playerPropertySystem.OnSatietyChanged
                .Subscribe(x =>
                {
                    //Debug.Log(x);
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
                    Debug.Log(x);
                    if (x == 0)
                    {
                        dependency.speed = 2;
                    }
                });
        }
    }
}
