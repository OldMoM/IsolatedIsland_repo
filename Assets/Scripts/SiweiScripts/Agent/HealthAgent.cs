using Peixi;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;


namespace Peixi
{
    public class HealthAgent
    {
        private AgentDependency dependency;

        public HealthAgent(AgentDependency Dependency)
        {
            dependency = Dependency;

            dependency.playerPropertySystem.OnPlayerDied.Subscribe(x => {
                Debug.Log("Player died");
            });

            // 根据心情改变健康值上限
            dependency.playerPropertySystem.OnPleasureLevelChanged
                .Subscribe(x =>
                {
                    Debug.Log(x);
                    switch (x)
                    {
                        case PropertyLevel.Euclid:
                            dependency.playerPropertySystem.MaxHealth = 80;
                            break;
                        case PropertyLevel.Keter:
                            dependency.playerPropertySystem.MaxHealth = 60;
                            break;
                        case PropertyLevel.Safe:
                            dependency.playerPropertySystem.MaxHealth = 100;
                            break;
                        default:
                            break;
                    }


                });
            // 根据饱腹值改变健康值
            dependency.playerPropertySystem.OnSatietyLevelChanged
                .Subscribe(x =>
                {
                    Debug.Log(x);
                    if (x == PropertyLevel.Keter)
                    {
                        dependency.playerPropertySystem.ChangeHealth(-10);
                    }
                    else { 
                        if(dependency.playerPropertySystem.SatietyLevel == PropertyLevel.Safe)
                        {
                            dependency.playerPropertySystem.ChangeHealth(5);
                        }
                    }
                });

            // 根据口渴值改变健康值
            dependency.playerPropertySystem.OnThirstLevelChanged
                .Subscribe(x =>
                {
                    Debug.Log(x);
                    if (x == PropertyLevel.Keter)
                    {
                        dependency.playerPropertySystem.ChangeHealth(-10);
                    }
                    else
                    {
                        if (dependency.playerPropertySystem.SatietyLevel == PropertyLevel.Safe)
                        {
                            dependency.playerPropertySystem.ChangeHealth(5);
                        }
                    }
                });


        }
    }
}
