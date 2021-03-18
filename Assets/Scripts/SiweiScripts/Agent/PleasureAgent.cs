using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace Peixi
{
    public class PleasureAgent
    {
        private AgentDependency dependency;

        private bool isSleep;

        public PleasureAgent(AgentDependency Dependency)
        {
            dependency = Dependency;

            //每12秒心情值-1(处于夜间且不休息)
            /*
            Observable.Interval(TimeSpan.FromSeconds(12))
            dependency.isDay
                .Subscribe(x=> {
                if (!x && !isSleep) { 
                    
                }
            })
            */

            //下雨时心情值-10
            dependency.onRainDay.Subscribe(x =>
            {
                dependency.playerPropertySystem.ChangePleasure(-10);
            });

            //根据心情值设定心情值等级
            dependency.playerPropertySystem.OnPleasureChanged
                .Subscribe(x =>
                {
                    //Debug.Log(x);
                    if (x > 30 && x < 50)
                    {
                        dependency.playerPropertySystem.PleasureLevel = PropertyLevel.Euclid;
                    }

                    else if (x <=30)
                    {
                        dependency.playerPropertySystem.PleasureLevel = PropertyLevel.Keter;
                    }

                    else // x>=50
                    {
                        dependency.playerPropertySystem.PleasureLevel = PropertyLevel.Safe;
                    }
                });

            // 心情值小于30时移速降低
            dependency.playerPropertySystem.OnPleasureChanged
                .Subscribe(x =>
                {
                    if (x <30)
                    {
                        dependency.speed = 2;
                    }
                });

            dependency.onRainDay
                .Skip(1)
                .Subscribe(x =>
                {
                    Debug.Log(1);
                    dependency.playerPropertySystem.ChangePleasure(-10);
                });
        }
    }
}
