using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
namespace Peixi
{
    public class AndroidraStateControllerAgent
    {
        public IObservable<(string, Vector2Int)> onBuildMsgReceived;
        public AndroidraStateController controller;
        public AndroidraControl control;

        public void Init()
        {
            //onBuildMsgReceived.Subscribe(x =>
            //{
            //    Debug.Log("received player's build command");
            //    controller.SetState(AndroidraState.Building,this.ToString());
            //});

            control.OnBuildMsgReceived
                .Subscribe(x =>
                {
                    Debug.Log("received player's build command");
                    controller.SetState(AndroidraState.Building, this.ToString());
                });
        }
    }
}
