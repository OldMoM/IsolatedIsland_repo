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

        private bool hungerSoundCd;
        public HungerAgent(AgentDependency Dependency)
        {
            dependency = Dependency;

            var hungerReduceRate = GameConfig.Singleton.InteractionConfig["playerSatietyReduceInterval"];
            //每6秒钟饱腹值-1
            Observable.Interval(TimeSpan.FromSeconds(hungerReduceRate))
                .Subscribe(x =>
                {
                    dependency.playerPropertySystem.ChangeSatiety(-1);
                });
            
            //根据饱腹值设定饱腹值等级
            dependency.playerPropertySystem.OnSatietyChanged
                .Subscribe(x =>
                {
                    if (x > 0 && x < 60)
                    {
                        dependency.playerPropertySystem.SatietyLevel = PropertyLevel.Euclid;

                        
                        if (hungerSoundCd)
                        {
                            AudioEvents.StartAudio("OnPlayerGetHungery");
                        }
                    }

                    if (x == 0)
                    {
                        dependency.playerPropertySystem.SatietyLevel = PropertyLevel.Keter;
                        AudioEvents.StartAudio("OnPlayerGetHungery");
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
                    dependency.speed = 2;
                    Debug.Log(dependency.GetHashCode());
                });

            Observable.Interval(TimeSpan.FromSeconds(10))
                .Subscribe(x =>
                {
                    hungerSoundCd = !hungerSoundCd;
                });
        }
    }
}
