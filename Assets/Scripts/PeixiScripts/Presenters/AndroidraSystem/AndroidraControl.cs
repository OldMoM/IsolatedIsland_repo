using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace Peixi
{
    public class AndroidraControl
    {
        /// <summary>
        /// Deliver facility's type at string, position at Vector2Int when build message is received
        /// </summary>
        public IObservable<ValueTuple<string, Vector2Int>> OnBuildMsgReceived => onBuildMsgReceived;

        private Subject<ValueTuple<string,Vector2Int>> onBuildMsgReceived = new Subject<ValueTuple<string, Vector2Int>>();

        public IObservable<bool> onReachTarget;
        public IObservable<Unit> onTimeOut;
        public AndroidraStateController stateController;

        public void BuildAt(string type, Vector2Int pos)
        {
            Debug.Log(1);
            onBuildMsgReceived.OnNext(new ValueTuple<string, Vector2Int>(type, pos));
            //return onReachTarget;

            onReachTarget
                .First()
                .Subscribe(x =>
                {
                    
                });

            onTimeOut
                .First()
                .Subscribe(x =>
                {
                    Debug.Log("虫丸成功修建了岛块");
                    stateController.SetState(AndroidraState.Follow, this.ToString());
                    InterfaceArichives.Archive.IBuildSystem.BuildIslandAt(pos);
                });
        }

        public void RestoreIsland(Vector2Int pos)
        {
            onBuildMsgReceived.OnNext(new ValueTuple<string, Vector2Int>("restore_island", pos));

            onReachTarget
                .First()
                .Subscribe(x =>
                {

                });

            onTimeOut
                .First()
                .Subscribe(x =>
                {
                    stateController.SetState(AndroidraState.Follow, this.ToString());

                    var islands = GameObject.FindObjectsOfType<IslandPresenter>();
                    islands.ToObservable()
                    .Where(y => y.data.gridPos == pos)
                    .Subscribe(y =>
                    {
                        y.Durability_current = 100;
                        Debug.Log("将" + pos + "岛块的耐久度重设为100");
                    });
                });
        }
    }
}
