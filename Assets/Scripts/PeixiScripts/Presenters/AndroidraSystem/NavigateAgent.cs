using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace Peixi
{
    public class NavigateAgent 
    {
        public AndroidraNavPresenter navPresenter;
        public AndroidraStateController stateController;
        public AndroidraControl control;
        public AndroidraBuildAnimationPresenter buildAnimation;
        public NavigateAgent(ref AndroidraNavPresenter androidraNavPresenter, 
                             ref AndroidraStateController androidraStateController,
                             ref AndroidraControl androidraControl)
        {
            navPresenter = androidraNavPresenter;
            stateController = androidraStateController;
            control = androidraControl;
        }

        private Vector2Int buildPos;
        public void Init()
        {
            navPresenter.OnReachedTarget
                .Where(x => stateController.State == AndroidraState.Building)
                .Where(x => x)
                .Skip(1)
                .Throttle(TimeSpan.FromSeconds(0.1f))
                .Subscribe(x =>
                {
                    Debug.Log("reached target and then start build island");
                    buildAnimation.StartPlayBuildAnimation();
                });

            control.OnBuildMsgReceived
              .Subscribe(x =>
              {
                  var targetWorldPos = InterfaceArichives.Archive.IBuildSystem.newGridToWorldPosition(x.Item2);
                  navPresenter.Target = targetWorldPos;
                  buildPos = x.Item2;
              });

            //buildAnimation.OnBuildAnimEnd
            //    .Subscribe(x =>
            //    {
            //        Debug.Log("end play build animation");
            //        stateController.SetState(AndroidraState.Follow, this.ToString());
            //        InterfaceArichives.Archive.IBuildSystem.BuildIslandAt(buildPos);
            //    });

        }
    }
}
