using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace Peixi 
{
    public class HungerAgent
    {
        private AgentDependency dependency;
        public HungerAgent(AgentDependency Dependency)
        {
            dependency = Dependency;

            //每6秒钟饱腹值-1
            Observable.Interval(TimeSpan.FromSeconds(6))
                .Subscribe(x =>
                {
                    dependency.playerPropertySystem.ChangeSatiety(-1);
                });
            
            //根据饱腹值设定饱腹值等级
            dependency.playerPropertySystem.OnSatietyChanged
                .Subscribe(x =>
                {
                    //Debug.Log(x);
                    if (x > 0 && x < 60)
                    {
                        dependency.playerPropertySystem.SatietyLevel = PropertyLevel.Euclid;

                        AudioEvents.StartAudio("OnPlayerGetHungery");
                    }

                    if (x == 0)
                    {
                        dependency.playerPropertySystem.SatietyLevel = PropertyLevel.Keter;

                        AudioEvents.StartAudio("OnPlayerGetExtremeHungery");
                    }

                    if (x >= 60)
                    {
                        dependency.playerPropertySystem.SatietyLevel = PropertyLevel.Safe;
                    }  
                });
            
            dependency.playerPropertySystem.OnSatietyChanged
                .Where(x => x==0)
                .Subscribe(x =>
                {
                    Debug.Log("sdfsdf");
                    dependency.speed = 2;
                    Debug.Log(dependency.GetHashCode());
                });
        }
    }
}
