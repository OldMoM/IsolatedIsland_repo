using Peixi;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using System;


namespace Peixi
{
    public class HealthAgent
    {
        private AgentDependency dependency;
        /// <summary>
        /// 由饮水量变化引起的健康值改变Buff
        /// </summary>
        public int thirstBuff = 0;
        /// <summary>
        /// 由饱腹值变化引起的健康值BUFF
        /// </summary>
        public int satietyBuff = 0;
        /// <summary>
        /// 各种Buff叠加后健康值总体变化量
        /// </summary>
        public int changeRate = 0;
        /// <summary>
        /// 玩家死亡事件
        /// </summary>
        public IObservable<bool> IsAlive => isAlive;

        private BoolReactiveProperty isAlive = new BoolReactiveProperty(true);
        
        public HealthAgent(AgentDependency Dependency)
        {
            dependency = Dependency;
   
            dependency.playerPropertySystem.OnPlayerDied
                .Subscribe(x =>
                {
                    dependency.onGameEnd.OnNext(Unit.Default);
                });

            // 根据心情改变健康值上限
            dependency.playerPropertySystem.OnPleasureLevelChanged
                .Skip(1)
                .Subscribe(x =>
                {
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

            #region//健康值对饮水量变化的响应行为
            dependency.playerPropertySystem.OnThirstChanged
                .Skip(1)
                .Subscribe(x =>
                {
                    if (x >= 70)//当饮水量=>70时，Health +5/min
                    {
                        thirstBuff = 5;
                    }
                    else if (x>0 && x<70)//当0<饮水量<70时，Health +0/min
                    {
                        thirstBuff = 0;
                    }
                    else if (x == 0)//当饮水量=0时，Health -10/min
                    {
                        thirstBuff = -10;
                    }
                    changeRate += thirstBuff;
                });
            #endregion

            dependency.playerPropertySystem.OnSatietyChanged
                .Skip(1)
                .Subscribe(x =>
                {
                    if (x >= 60)//当饱腹量=>60时，Health +5/min
                    {
                        satietyBuff = 5;
                    }
                    else if (x<60 && x>0)
                    {
                        satietyBuff = 0;

                    }
                    else if (x == 0)
                    {
                        satietyBuff = -10;
                    }
                    changeRate += satietyBuff;
                });

            Observable.Interval(TimeSpan.FromSeconds(1))
                .Subscribe(x =>
                {
                    dependency.playerPropertySystem.ChangeHealth(changeRate);
                });

            dependency.playerPropertySystem.OnHealthChanged
                .Where(x => x == 0)
                .Subscribe(x =>
                {
                    isAlive.Value = false;
                });

            //当生命值<=30，触发心跳声
            dependency.playerPropertySystem.OnHealthChanged
                .Where(x => x <= 30)
                .Subscribe(x =>
                {
                    AudioEvents.StartAudio("OnPlayerGetExhausted");
                });
        }
    }
}
