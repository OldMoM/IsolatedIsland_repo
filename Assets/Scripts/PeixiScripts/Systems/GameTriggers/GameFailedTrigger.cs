using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

namespace Peixi 
{
    public static class GameFailedTrigger
    {
        /// <summary>
        ///   <para>
        /// 饥饿值归0超过15秒，判定游戏结束</para>
        ///   <para>在此期间饥饿值回复则取消判定</para>
        /// </summary>
        /// <param name="gameTriggers">The game triggers.</param>
        /// <param name="onHealthChanged">The on health changed.</param>
        public static void OnGameFailedTrigged(IObservable<int> onHungerChanged)
        {
            onHungerChanged.Where(x => x <= 0)
                .Subscribe(x =>
                {
                    Debug.Log(x);
                    EnvironmentModel.isDeathCountDown = true;
                    EnvironmentModel.deathCountDown = 0;
                });

            onHungerChanged.Where(x => x > 0)
                .Subscribe(x =>
                {
                    Debug.Log(x);
                    EnvironmentModel.isDeathCountDown = false;
                });


            var deathCountDown =
            Observable.Interval(TimeSpan.FromSeconds(1))
                .Where(x => EnvironmentModel.isDeathCountDown);

            IDisposable release = null;
            release =
            deathCountDown
                .Subscribe(x =>
                {
                    EnvironmentModel.deathCountDown++;
                    if (EnvironmentModel.deathCountDown >= 5)
                    {
                        GameTriggerModel.gameTriggers["gameFailed"].OnNext(Unit.Default);
                        Debug.Log("游戏结束");
                        release.Dispose();
                    }
                });
        }
    }
}
